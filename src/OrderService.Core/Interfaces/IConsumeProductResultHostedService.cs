using Microsoft.Extensions.Hosting;
using OrderService.Core.RabbitMqDto;

namespace OrderService.Core.Interfaces;
public interface IConsumeProductResultHostedService: IHostedService
{
  public void InitRabbitMQ();

  public void ConsumeMessage(string message);

  public void SendToQueue(RabbitRequestProductData rabbitRequestProductData);
}
