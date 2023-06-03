using MediatR;
using Microsoft.Extensions.Configuration;
using OrderService.Core.Interfaces;
using OrderService.Core.OrderAggregate.Events;
namespace OrderService.Core.OrderAggregate.Handlers;
public class OrderDetailUpdatedWeightHandler : INotificationHandler<OrderDetailUpdatedWeightEvent>
{

  private readonly IEmailSender _emailSender;
  private readonly IConfiguration _configuration;
  public OrderDetailUpdatedWeightHandler(IEmailSender emailSender, IConfiguration configuration)
  {
    _emailSender = emailSender;
    _configuration = configuration;
  }

  public Task Handle(OrderDetailUpdatedWeightEvent notification, CancellationToken cancellationToken)
  {
    _emailSender.SendEmail(notification.order.user.email, "[FastShip] Cập nhật trọng lượng sản phẩm", $"<p>Xin chào bạn, đơn hàng #{notification.order.Id} đã được cập nhật trọng lượng sản phẩm {notification.product.productName} thành {notification.product.productWeight}kg vì vậy tổng giá trị đơn hàng đã được thay đổi thành {notification.order.price} <a href='{_configuration["SERVER_ORIGIN"]}/detailod?orderId={notification.order.Id}'>Để xem chi tiết vui lòng nhấn vào đây</a></p>");

    return Task.CompletedTask;
  }
}
