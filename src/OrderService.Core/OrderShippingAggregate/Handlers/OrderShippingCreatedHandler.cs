using MediatR;
using OrderService.Core.Interfaces;
using OrderService.Core.OrderAggregate;
using OrderService.Core.OrderAggregate.Specifications;
using OrderService.Core.OrderPaymentAggregate;
using OrderService.Core.OrderShippingAggregate.Events;
using OrderService.SharedKernel.Interfaces;

namespace OrderService.Core.OrderShippingAggregate.Handlers;
public class OrderShippingCreatedHandler : INotificationHandler<OrderShippingCreatedEvent>
{

  private readonly IRepository<Order> _orderRepository;

  private readonly IEmailSender _emailSender;

  public OrderShippingCreatedHandler(IRepository<Order> orderRepository, IEmailSender emailSender)
  {
    _orderRepository = orderRepository;
    _emailSender = emailSender;
  }



  public async Task Handle(OrderShippingCreatedEvent notification, CancellationToken cancellationToken)
  {
    var orderSpec = new OrderChatByIdSpec(notification.OrderId);
    var order = await _orderRepository.FirstOrDefaultAsync(orderSpec);

    if (order == null)
    {
      throw new Exception("Order is not found");
    }

    await _emailSender.SendEmailAsync(order.user.email, "[FastShip] Hàng đang đến bạn", $"<p>Xin chào bạn, đơn hàng #{notification.OrderId} đã được giao cho shipper để đưa đến bạn <a href='http://localhost:3000/detailod?orderId={notification.OrderId}'>Để xem chi tiết vui lòng nhấn vào đây</a></p>");


    await _orderRepository.SaveChangesAsync();
  }
}
