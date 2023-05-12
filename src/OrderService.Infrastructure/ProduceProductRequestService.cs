using Newtonsoft.Json;
using System.Text;
using OrderService.Core.Interfaces;
using OrderService.Core.RabbitMqDto;
using RabbitMQ.Client;
using Microsoft.Extensions.Configuration;

namespace OrderService.Infrastructure;
public class ProduceProductRequestService : IProduceProductRequestService
{
  IConnection connection;
  IModel channel;


  public ProduceProductRequestService(IConfiguration configuration)
  {
    var factory = new ConnectionFactory { HostName = configuration["HOSTNAME"] };
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

    Console.WriteLine("init producer instance");
  }

  public void SendToResultQueue(RabbitResponseProductData rabbitResponseProductData)
  {
    var json = JsonConvert.SerializeObject(rabbitResponseProductData);

    var body = Encoding.UTF8.GetBytes(json);

    channel.BasicPublish(exchange: string.Empty,
    routingKey: "eshop_result",
    basicProperties: null,
    body: body);
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
