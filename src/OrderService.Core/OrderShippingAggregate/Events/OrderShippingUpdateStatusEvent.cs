using OrderService.SharedKernel;

namespace OrderService.Core.OrderShippingAggregate.Events;
public class OrderShippingUpdateStatusEvent : DomainEventBase
{
  public int orderId { get; set; } 
  public OrderShippingStatus orderShippingStatus { get; set; }

  public OrderShippingUpdateStatusEvent(int orderId, OrderShippingStatus orderShippingStatus)
  {
    this.orderId = orderId;
    this.orderShippingStatus = orderShippingStatus;
  }
}
