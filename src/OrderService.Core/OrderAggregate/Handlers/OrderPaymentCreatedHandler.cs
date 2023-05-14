using MediatR;
using OrderService.Core.Interfaces;
using OrderService.Core.OrderAggregate.Events;
using OrderService.Core.OrderAggregate.Specifications;
using OrderService.SharedKernel.Interfaces;

namespace OrderService.Core.OrderAggregate.Handlers;
public class OrderPaymentCreatedHandler : INotificationHandler<OrderPaymentCreatedEvent>
{


  private readonly IRepository<Order> _repository;

  private readonly IEmailSender _emailSender;

 
  public OrderPaymentCreatedHandler(IRepository<Order> repository, IEmailSender emailSender)
  {
    _repository = repository;
    _emailSender = emailSender;
  }

  public async Task Handle(OrderPaymentCreatedEvent notification, CancellationToken cancellationToken)
  {
    var orderSpec = new OrderPaymentChatByIdSpec(notification.OrderId);
    var order = await _repository.FirstOrDefaultAsync(orderSpec);

    var orderPayment = order!.orderPayments.Where(op => op.Id == notification.PaymentId).FirstOrDefault();

    if (orderPayment == null)
    {
      throw new NullReferenceException("order payment is null");
    }

    if (orderPayment.paymentStatus == PaymentStatus.firstPayment)
    {
      order.SetStatus(OrderStatus.waitingToOrderFromSeller);
    }

    order.SetRemainCost(order.remainCost - orderPayment.paymentCost);

    await _emailSender.SendEmailAsync(order.user.email, "[FastShip] Cập nhật trạng thái đơn hàng", $"<p>Xin chào bạn, đơn hàng #{notification.OrderId} đã được thanh toán thành công với số tiền: {orderPayment.paymentCost}! <a href='http://localhost:3000/detailod?orderId={notification.OrderId}'>Để xem chi tiết vui lòng nhấn vào đây</a></p>");

    await _repository.SaveChangesAsync();
  }
}
