using MediatR;
using Microsoft.Extensions.Configuration;
using OrderService.Core.Interfaces;
using OrderService.Core.OrderAggregate.Events;
using OrderService.SharedKernel.Interfaces;

namespace OrderService.Core.OrderAggregate.Handlers;
public class OrderDetailUpdateEventHandler : INotificationHandler<OrderDetailUpdateEvent>
{
  private readonly IRepository<Order> _orderRepository;
  private readonly IEmailSender _emailSender;
  private readonly IConfiguration _configuration;

  public OrderDetailUpdateEventHandler(IRepository<Order> orderRepository, IEmailSender emailSender, IConfiguration configuration)
  {
    _orderRepository = orderRepository;
    _emailSender = emailSender;
    _configuration = configuration;
  }

  public async Task Handle(OrderDetailUpdateEvent notification, CancellationToken cancellationToken)
  {
    var order = notification.order;

    double totalCost = 0.0f;

    float totalCostOfOrderDetail;

    foreach (var orderDetail in order.orderDetails)
    {
      totalCostOfOrderDetail = orderDetail.product.productShipCost;
      totalCostOfOrderDetail += orderDetail.product.productPrice * orderDetail.quantity;
      totalCostOfOrderDetail += orderDetail.additionalCost;
      totalCostOfOrderDetail += orderDetail.processCost;

      orderDetail.setTotalCost(totalCostOfOrderDetail);

      totalCost += totalCostOfOrderDetail * orderDetail.product.currencyExchange.rate;
    }

    totalCost = Math.Ceiling(totalCost);

    order.SetPrice(totalCost);

    await _orderRepository.SaveChangesAsync();

    _emailSender.SendEmail(order.user.email, "[FastShip] Cập nhật giá mới", $"<p>Đơn hàng của bạn có cập nhật mới!  <a href='{_configuration["SERVER_ORIGIN"]}/detailod?orderId={order.Id}'>Để xem chi tiết vui lòng nhấn vào đây</a></p>");
  }
}
