﻿using Newtonsoft.Json;
using System.Text;
using OrderService.Core.Interfaces;
using OrderService.Core.RabbitMqDto;
using RabbitMQ.Client;

namespace OrderService.Infrastructure;
public class ProduceProductRequestService : IProduceProductRequestService
{
  IConnection connection;
  IModel channel;


  public ProduceProductRequestService()
  {
    var factory = new ConnectionFactory { HostName = "host.docker.internal" };
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
