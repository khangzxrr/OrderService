using System.Text;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using OrderService.Core.CurrencyAggregate;
using OrderService.Core.CurrencyAggregate.Specifications;
using OrderService.Core.Interfaces;
using OrderService.Core.ProductAggregate;
using OrderService.Core.ProductAggregate.Specifications;
using OrderService.Core.RabbitMqDto;
using OrderService.SharedKernel.Interfaces;
using OrderService.Web.SignalR;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using StackExchange.Redis.Extensions.Core.Abstractions;

namespace OrderService.Web;
public class ConsumeProductResultHostedService : BackgroundService, IConsumeProductResultHostedService
{
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
  IConnection connection;
  IModel channel;



  private readonly IRepository<ProductCategory> _categoryRepository;
  private readonly IRepository<Product> _productRepository;
  private readonly IRepository<CurrencyExchange> _currencyExchange;
  private readonly IHubContext<ProductFetchingHub>  _notificationHub;
  private readonly IConfiguration _configuration;

  private readonly IRedisClient _redisClient;

  private readonly IMediaService _mediaService;

  public ConsumeProductResultHostedService(IRepository<ProductCategory> categoryRepository, IRepository<Product> productRepository, IRepository<CurrencyExchange> currencyExchange, IHubContext<ProductFetchingHub> notificationHub, IConfiguration configuration, IRedisClient redisClient, IMediaService mediaService)
  {
    _categoryRepository = categoryRepository;
    _productRepository = productRepository;
    _currencyExchange = currencyExchange;
    _notificationHub = notificationHub;
    _configuration = configuration;

    _redisClient = redisClient;

    Console.WriteLine("init rabbitmq");
    InitRabbitMQ();
    _mediaService = mediaService;
  }

#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.


  public async void ConsumeMessage(string message)
  {
    if (message.Contains("message"))
    {
      var messageObject = JsonConvert.DeserializeObject<MessageData>(message);
      await ProductFetchingHub.SendPrivateMessage(_notificationHub.Clients, messageObject!.connectionId, message);

      return;
    }

    var productResult = JsonConvert.DeserializeObject<RabbitResponseProductData>(message);

    var catalogSpec = new ProductCatalogByNameSpec(productResult!.catalog);
    ProductCategory? category = await _categoryRepository.FirstOrDefaultAsync(catalogSpec);

    var currencySpec = new CurrencyExchangeByName("US");
    var currency = await _currencyExchange.FirstOrDefaultAsync(currencySpec);

    if (category == null)
    {
      Console.WriteLine("add new catalog");

      category = new ProductCategory(productResult!.catalog);

      category = await _categoryRepository.AddAsync(category);
    }

    Console.WriteLine("add new product");

    var uploadImage = await _mediaService.downloadImage(productResult!.imageUrl);

    var product = new Product(
      productResult!.product,
      uploadImage,
      "yo this is description",
      (float)productResult!.price,
      (float)productResult!.shipCost,
      12.0f,
      productResult!.url,
      0,
      false,
      "warranty description",
      0,
      (productResult!.returnDays > 0),
      $"accept {productResult!.returnDays} return days",
      productResult!.returnDays);

    product.setProductCategory(category);


    var productCurrentExchange = new ProductCurrencyExchange(currency!.rate);

    productCurrentExchange.setProduct(product);
    productCurrentExchange.setCurrency(currency);

    product.setCurrencyExchange(productCurrentExchange);

    await _productRepository.AddAsync(product);
    await _productRepository.SaveChangesAsync();


    //PUSH TO redis cache

    string generatedProductRedisHashKey = product.generateRedisHashKey();

    await _redisClient.Db0.AddAsync(generatedProductRedisHashKey, productResult);

    await _redisClient.Db0.UpdateExpiryAsync(generatedProductRedisHashKey, DateTimeOffset.Now.AddMinutes(60));

    //=====

    //send data back to client
    await ProductFetchingHub.SendPrivateMessage(_notificationHub.Clients, productResult.connectionId, JsonConvert.SerializeObject(product));
    
  }

  public void InitRabbitMQ()
  {
    var factory = new ConnectionFactory { HostName = _configuration["HOSTNAME"], Port = 5672 };

    Console.WriteLine(factory.HostName);
    
    connection = factory.CreateConnection();
    channel = connection.CreateModel();

    channel.QueueDeclare(queue: "eshop_result",
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null);

    channel.QueueDeclare(queue: "eshop_queue",
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null);


  }

  protected override Task ExecuteAsync(CancellationToken stoppingToken)
  {


    Console.WriteLine("activate consumer rabbitmq");

    var consumeResult = new EventingBasicConsumer(channel);
    consumeResult.Received += (model, ea) =>
    {
      var body = ea.Body.ToArray();
      var jsonProduct = Encoding.UTF8.GetString(body);

      try
      {
        ConsumeMessage(jsonProduct);
        channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex);
        channel.BasicNack(ea.DeliveryTag, false, true);
      }
    };

    channel.BasicConsume(queue: "eshop_result", autoAck: false, consumer: consumeResult);

    return Task.CompletedTask;
  }

  public override void Dispose()
  {
    channel.Dispose();
    connection.Dispose();

    base.Dispose();
  }

  public void SendToQueue(RabbitRequestProductData rabbitProductRequest)
  {
    var json = JsonConvert.SerializeObject(rabbitProductRequest);

    var body = Encoding.UTF8.GetBytes(json);
    channel.BasicPublish(exchange: string.Empty,
    routingKey: "eshop_queue",
    basicProperties: null,
    body: body);
  }
}
