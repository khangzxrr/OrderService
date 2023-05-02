using MediatR;
using OrderService.Core.OrderAggregate;
using OrderService.Core.OrderAggregate.Specifications;
using OrderService.Core.OrderShippingAggregate.Events;
using OrderService.SharedKernel.Interfaces;

namespace OrderService.Core.OrderShippingAggregate.Handlers;
public class OrderShippingUpdateStatusHandler : INotificationHandler<OrderShippingUpdateStatusEvent>
{

  private readonly IRepository<Order> _orderRepository;

  public OrderShippingUpdateStatusHandler(IRepository<Order> orderRepository)
  {
    _orderRepository = orderRepository;
  }

  public async Task Handle(OrderShippingUpdateStatusEvent notification, CancellationToken cancellationToken)
  {
    var orderSpec = new OrderChatByIdSpec(notification.orderId);
    var order = await _orderRepository.FirstOrDefaultAsync(orderSpec);

    if (order == null) {
      throw new Exception("order is not found");
    }

    order.chat.AddNewNotifiChatMessage($"Updated delivery status to  {notification.orderShippingStatus.Name} At {DateTime.Now:HH:ss dd/MM/yyyy}");

    await _orderRepository.SaveChangesAsync();
  }
}
