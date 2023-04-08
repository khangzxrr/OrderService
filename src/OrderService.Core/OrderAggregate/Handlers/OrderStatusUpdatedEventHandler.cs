using MediatR;
using OrderService.Core.OrderAggregate.Events;
using OrderService.Core.OrderAggregate.Specifications;
using OrderService.SharedKernel.Interfaces;

namespace OrderService.Core.OrderAggregate.Handlers;
public class OrderStatusUpdatedEventHandler : INotificationHandler<OrderStatusUpdatedEvent>
{

  private readonly IRepository<Order> _orderRepository;

  public OrderStatusUpdatedEventHandler(IRepository<Order> orderRepository)
  {
    _orderRepository = orderRepository;
  }

  public async Task Handle(OrderStatusUpdatedEvent notification, CancellationToken cancellationToken)
  {
    var orderSpec = new OrderPaymentChatByIdSpec(notification.OrderId);
    var order = await _orderRepository.FirstOrDefaultAsync(orderSpec);

    if (order == null)
    {
      return;
    }

    order.chat.AddNewNotifiChatMessage($"Updated order status to {notification.orderStatus.Name} at {DateTime.Now.ToString("HH:ss dd/MM/yyyy")}");

    await _orderRepository.SaveChangesAsync();
  }
}
