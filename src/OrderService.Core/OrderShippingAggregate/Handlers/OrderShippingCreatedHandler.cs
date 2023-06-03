using MediatR;
using Microsoft.Extensions.Configuration;
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

  private readonly IConfiguration _configuration;

  public OrderShippingCreatedHandler(IRepository<Order> orderRepository, IEmailSender emailSender, IConfiguration configuration)
  {
    _orderRepository = orderRepository;
    _emailSender = emailSender;
    _configuration = configuration;
  }



  public async Task Handle(OrderShippingCreatedEvent notification, CancellationToken cancellationToken)
  {
    var orderSpec = new OrderChatByIdSpec(notification.OrderId);
    var order = await _orderRepository.FirstOrDefaultAsync(orderSpec);

    if (order == null)
    {
      throw new Exception("Order is not found");
    }

    _emailSender.SendEmail(order.user.email, "[FastShip] Hàng đang đến bạn", $"<p>Xin chào bạn, đơn hàng #{notification.OrderId} đã được giao cho shipper để đưa đến bạn <a href='{_configuration["SERVER_ORIGIN"]}/detailod?orderId={notification.OrderId}'>Để xem chi tiết vui lòng nhấn vào đây</a></p>");


    await _orderRepository.SaveChangesAsync();
  }
}
