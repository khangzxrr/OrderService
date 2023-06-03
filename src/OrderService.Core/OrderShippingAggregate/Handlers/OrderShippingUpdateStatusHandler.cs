using MediatR;
using Microsoft.Extensions.Configuration;
using OrderService.Core.Interfaces;
using OrderService.Core.OrderAggregate;
using OrderService.Core.OrderAggregate.Specifications;
using OrderService.Core.OrderPaymentAggregate;
using OrderService.Core.OrderShippingAggregate.Events;
using OrderService.SharedKernel.Interfaces;

namespace OrderService.Core.OrderShippingAggregate.Handlers;
public class OrderShippingUpdateStatusHandler : INotificationHandler<OrderShippingUpdateStatusEvent>
{

  private readonly IRepository<Order> _orderRepository;

  private readonly IEmailSender _emailSender;

  private readonly IConfiguration _configuration;
  public OrderShippingUpdateStatusHandler(IRepository<Order> orderRepository, IEmailSender emailSender, IConfiguration configuration)
  {
    _orderRepository = orderRepository;
    _emailSender = emailSender;
    _configuration = configuration;
  }

  public async Task Handle(OrderShippingUpdateStatusEvent notification, CancellationToken cancellationToken)
  {
    var orderSpec = new OrderChatByIdSpec(notification.orderId);
    var order = await _orderRepository.FirstOrDefaultAsync(orderSpec);

    if (order == null) {
      throw new Exception("order is not found");
    }

    _emailSender.SendEmail(order.user.email, "[FastShip] Cập nhật trạng thái giao hàng", $"<p> đã được giao cho shipper để đưa đến bạn được cập nhật trạng thái mới ({notification.orderShippingStatus.Name}) <a href='{_configuration["SERVER_ORIGIN"]}/detailod?orderId={notification.orderId}'>Để xem chi tiết vui lòng nhấn vào đây</a></p>");

    await _orderRepository.SaveChangesAsync();
  }
}
