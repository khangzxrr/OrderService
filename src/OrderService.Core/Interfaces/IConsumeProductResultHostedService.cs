using Microsoft.Extensions.Hosting;

namespace OrderService.Core.Interfaces;
public interface IConsumeProductResultHostedService: IHostedService
{
  public void InitRabbitMQ();

  public void ConsumeMessage(string message);

  public void SendToQueue(string url);
}
