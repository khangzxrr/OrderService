using MediatR;
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

  public OrderShippingUpdateStatusHandler(IRepository<Order> orderRepository, IEmailSender emailSender)
  {
    _orderRepository = orderRepository;
    _emailSender = emailSender;
  }

  public async Task Handle(OrderShippingUpdateStatusEvent notification, CancellationToken cancellationToken)
  {
    var orderSpec = new OrderChatByIdSpec(notification.orderId);
    var order = await _orderRepository.FirstOrDefaultAsync(orderSpec);

    if (order == null) {
      throw new Exception("order is not found");
    }

    await _emailSender.SendEmailAsync(order.user.email, "[FastShip] Cập nhật trạng thái giao hàng", $"<p> đã được giao cho shipper để đưa đến bạn được cập nhật trạng thái mới ({notification.orderShippingStatus.Name}) <a href='http://localhost:3000/detailod?orderId={notification.orderId}'>Để xem chi tiết vui lòng nhấn vào đây</a></p>");

    await _orderRepository.SaveChangesAsync();
  }
}
