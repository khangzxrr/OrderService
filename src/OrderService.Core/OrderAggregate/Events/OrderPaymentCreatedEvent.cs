using OrderService.Core.OrderAggregate.Handlers;
using OrderService.SharedKernel;

namespace OrderService.Core.OrderAggregate.Events;
public class OrderPaymentCreatedEvent : DomainEventBase
{
  public int OrderId { get; set; }
  public int PaymentId { get; set; }

  public OrderPaymentCreatedEvent(int orderId, int paymentId)
  {
    OrderId = orderId;
    PaymentId = paymentId;
  }
}
