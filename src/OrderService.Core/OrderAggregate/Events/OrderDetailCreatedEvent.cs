using OrderService.SharedKernel;

namespace OrderService.Core.OrderAggregate.Events;
public class OrderDetailCreatedEvent : DomainEventBase
{
  public int OrderId { get; set; } 

  public OrderDetailCreatedEvent(int OrderId)
  {
    this.OrderId = OrderId;
  }
}
