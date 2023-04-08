using OrderService.SharedKernel;

namespace OrderService.Core.OrderAggregate.Events;
public class OrderStatusUpdatedEvent : DomainEventBase
{

  public int OrderId { get; set; }
  public OrderStatus orderStatus { get; set; }

  public OrderStatusUpdatedEvent(int orderId, OrderStatus orderStatus)
  {
    OrderId = orderId;
    this.orderStatus = orderStatus;
  }
}
