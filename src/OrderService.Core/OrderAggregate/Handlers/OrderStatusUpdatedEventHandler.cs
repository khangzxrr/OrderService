using MediatR;
using Microsoft.Extensions.Configuration;
using OrderService.Core.Interfaces;
using OrderService.Core.OrderAggregate.Events;
using OrderService.Core.OrderAggregate.Specifications;
using OrderService.SharedKernel.Interfaces;

namespace OrderService.Core.OrderAggregate.Handlers;
public class OrderStatusUpdatedEventHandler : INotificationHandler<OrderStatusUpdatedEvent>
{

  private readonly IRepository<Order> _orderRepository;

  private readonly IEmailSender _emailSender;

  private readonly IConfiguration _configuration;

  public OrderStatusUpdatedEventHandler(IRepository<Order> orderRepository, IEmailSender emailSender, IConfiguration configuration)
  {
    _orderRepository = orderRepository;
    _emailSender = emailSender;
    _configuration = configuration;
  }

  public async Task Handle(OrderStatusUpdatedEvent notification, CancellationToken cancellationToken)
  {
    var orderSpec = new OrderPaymentChatByIdSpec(notification.OrderId);
    var order = await _orderRepository.FirstOrDefaultAsync(orderSpec);

    if (order == null)
    {
      return;
    }


    _emailSender.SendEmail(order.user.email, "[FastShip] Cập nhật trạng thái đơn hàng", $"<p>Xin chào bạn, đơn hàng #{notification.OrderId} vừa được cập nhật trạng thái mới ({notification.orderStatus}) <a href='{_configuration["SERVER_ORIGIN"]}/detailod?orderId={notification.OrderId}'>Để xem chi tiết vui lòng nhấn vào đây</a></p>");

    await _orderRepository.SaveChangesAsync();
  }
}
