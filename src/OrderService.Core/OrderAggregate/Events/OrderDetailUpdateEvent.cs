using OrderService.SharedKernel;

namespace OrderService.Core.OrderAggregate.Events;
public class OrderDetailUpdateEvent : DomainEventBase
{
  public Order order{ get; } 

  public OrderDetailUpdateEvent(Order order)
  {
    this.order = order;
  }
}
