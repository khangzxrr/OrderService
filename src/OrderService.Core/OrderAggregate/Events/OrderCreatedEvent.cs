using OrderService.SharedKernel;

namespace OrderService.Core.OrderAggregate.Events;
public class OrderCreatedEvent : DomainEventBase
{
  public int OrderId { get; set; } 

  public OrderCreatedEvent(int OrderId)
  {
    this.OrderId = OrderId;
  }
}
