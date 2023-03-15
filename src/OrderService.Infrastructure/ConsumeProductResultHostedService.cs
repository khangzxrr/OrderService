using System.Text;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using OrderService.Core.CurrencyAggregate;
using OrderService.Core.CurrencyAggregate.Specifications;
using OrderService.Core.Interfaces;
using OrderService.Core.ProductAggregate;
using OrderService.Core.ProductAggregate.Specifications;
using OrderService.SharedKernel.Interfaces;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace OrderService.Infrastructure;
public class ConsumeProductResultHostedService : BackgroundService, IConsumeProductResultHostedService
{
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
  IConnection connection;
  IModel channel;



  private readonly IRepository<ProductCategory> _categoryRepository;
  private readonly IRepository<Product> _productRepository;
  private readonly IRepository<CurrencyExchange> _currencyExchange;


  public ConsumeProductResultHostedService(IRepository<ProductCategory> categoryRepository, IRepository<Product> productRepository, IRepository<CurrencyExchange> currencyExchange)
  {
    _categoryRepository = categoryRepository;
    _productRepository = productRepository;
    _currencyExchange = currencyExchange;

    Console.WriteLine("init rabbitmq");
    InitRabbitMQ();

  }

#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

  public async void ConsumeMessage(string message)
  {
    var productResult = JsonConvert.DeserializeObject<ProductResult>(message);
    Console.WriteLine(productResult);



    var catalogSpec = new ProductCatalogByNameSpec(productResult!.Catalog);
    ProductCategory? category = await _categoryRepository.FirstOrDefaultAsync(catalogSpec);
    ProductShipCost? productShipCost = (category != null) ? category.productShipCost : null;
    if (category == null)
    {
      Console.WriteLine("add new catalog");

      category = new ProductCategory(productResult!.Catalog);

      productShipCost = new ProductShipCost(10.0f, 12.0f, "[  {    \"WorkflowName\": \"AdditionalCost\",    \"Rules\": [      {        \"RuleName\": \"Price_over\",        \"Enabled\": true,        \"Expression\": \"orderDetail.productCost> 200\",        \"Actions\": {\t    \"OnSuccess\": {\"Name\": \"OutputAdditionalCost\",\"Content\": {   \"Expression\": \"orderDetail.productCost * 0.05\"\t    },\t    \"OnFailure\": {\"Name\": \"OutputAdditionalCost\",\"Content\": {   \"Expression\": \"0\"                }\t    }        }      }    ]  }]");
      category.SetProductShipCost(productShipCost);

      category = await _categoryRepository.AddAsync(category);
    }

    var productSpec = new ProductByUrlSpec(productResult!.Url);
    var product = await _productRepository.FirstOrDefaultAsync(productSpec);

    if (product == null)
    {
      Console.WriteLine("add new product");

      product = new Product(
        productResult!.Product,
        "https://picsum.photos/200",
        "yo this is description",
        300,
        productResult!.Url,
        0,
        "seller address",
        "seller email",
        false,
        "warranty description",
        0,
        false,
        "return description",
        0);

      product.setProductCategory(category);

      var currencySpec = new CurrencyExchangeByName("US");
      var currency = await _currencyExchange.FirstOrDefaultAsync(currencySpec);

      product.setCurrencyExchange(currency!);

      await _productRepository.AddAsync(product);
      await _productRepository.SaveChangesAsync();
    }



  }

  public void InitRabbitMQ()
  {
    var factory = new ConnectionFactory { HostName = "localhost" };
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
    stoppingToken.ThrowIfCancellationRequested();

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

  public void SendToQueue(string url)
  {
    var body = Encoding.UTF8.GetBytes(url);
    channel.BasicPublish(exchange: string.Empty,
    routingKey: "eshop_queue",
    basicProperties: null,
    body: body);
  }
}
