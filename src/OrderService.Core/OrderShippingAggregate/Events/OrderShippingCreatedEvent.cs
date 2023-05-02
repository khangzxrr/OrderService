using OrderService.SharedKernel;

namespace OrderService.Core.OrderShippingAggregate.Events;
public class OrderShippingCreatedEvent : DomainEventBase
{
  public int OrderId { get; set; } 

  public bool IsUsing3rd { get; set; }

  public OrderShippingCreatedEvent(int orderId, bool isUsing3rd)
  {
    OrderId = orderId;
    IsUsing3rd = isUsing3rd;
  }
}
