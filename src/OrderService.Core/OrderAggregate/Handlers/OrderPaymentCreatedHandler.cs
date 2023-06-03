using MediatR;
using Microsoft.Extensions.Configuration;
using OrderService.Core.Interfaces;
using OrderService.Core.OrderAggregate.Events;
using OrderService.Core.OrderAggregate.Specifications;
using OrderService.SharedKernel.Interfaces;

namespace OrderService.Core.OrderAggregate.Handlers;
public class OrderPaymentCreatedHandler : INotificationHandler<OrderPaymentCreatedEvent>
{


  private readonly IRepository<Order> _repository;

  private readonly IEmailSender _emailSender;
  private readonly IConfiguration _configuration;

 
  public OrderPaymentCreatedHandler(IRepository<Order> repository, IEmailSender emailSender, IConfiguration configuration)
  {
    _repository = repository;
    _emailSender = emailSender;
    _configuration = configuration;
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

    _emailSender.SendEmail(order.user.email, "[FastShip] Cập nhật thanh toán", $"<p>Xin chào bạn, đơn hàng #{notification.OrderId} đã được thanh toán thành công với số tiền: {orderPayment.paymentCost}! <a href='{_configuration["SERVER_ORIGIN"]}/detailod?orderId={notification.OrderId}'>Để xem chi tiết vui lòng nhấn vào đây</a></p>");

    await _repository.SaveChangesAsync();
  }
}
