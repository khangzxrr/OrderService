using MediatR;
using OrderService.Core.OrderAggregate.Events;
using OrderService.Core.OrderAggregate.Specifications;
using OrderService.SharedKernel.Interfaces;

namespace OrderService.Core.OrderAggregate.Handlers;
public class OrderPaymentCreatedHandler : INotificationHandler<OrderPaymentCreatedEvent>
{


  private readonly IRepository<Order> _repository;

  public OrderPaymentCreatedHandler(IRepository<Order> repository)
  {
    _repository = repository;
  }

  public async Task Handle(OrderPaymentCreatedEvent notification, CancellationToken cancellationToken)
  {
    var orderSpec = new OrderById(notification.OrderId);
    var order = await _repository.FirstOrDefaultAsync(orderSpec);

    var orderPayment = order!.orderPayments.Where(op => op.Id == notification.PaymentId).FirstOrDefault();

    order.chat.AddNewNotifiChatMessage(
      $"Payment successfully, amount: {orderPayment!.paymentCost} at {orderPayment.paymentDate.ToString("HH:ss dd/MM/yyyy")}");

    if (orderPayment.paymentStatus == PaymentStatus.firstPayment)
    {
      order.SetStatus(OrderStatus.waitingToOrderFromSeller);
    }

    await _repository.SaveChangesAsync();
  }
}
