using OrderService.Core.RabbitMqDto;

namespace OrderService.Core.Interfaces;
public interface IProduceProductRequestService
{
  public void SendToQueue(RabbitRequestProductData rabbitRequestProductData);
}
