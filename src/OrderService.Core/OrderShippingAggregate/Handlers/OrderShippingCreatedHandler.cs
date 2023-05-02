using MediatR;
using OrderService.Core.OrderAggregate;
using OrderService.Core.OrderAggregate.Specifications;
using OrderService.Core.OrderShippingAggregate.Events;
using OrderService.SharedKernel.Interfaces;

namespace OrderService.Core.OrderShippingAggregate.Handlers;
public class OrderShippingCreatedHandler : INotificationHandler<OrderShippingCreatedEvent>
{

  private readonly IRepository<Order> _orderRepository;

  public OrderShippingCreatedHandler(IRepository<Order> orderRepository)
  {
    _orderRepository = orderRepository;
  }



  public async Task Handle(OrderShippingCreatedEvent notification, CancellationToken cancellationToken)
  {
    var orderSpec = new OrderChatByIdSpec(notification.OrderId);
    var order = await _orderRepository.FirstOrDefaultAsync(orderSpec);

    if (order == null)
    {
      throw new Exception("Order is not found");
    }

    order.chat.AddNewNotifiChatMessage("Delivering product by using " + 
      (notification.IsUsing3rd ? "3rd shipping" : "company shipper") +
      $" at {DateTime.Now:HH:ss dd/MM/yyyy}");

    await _orderRepository.SaveChangesAsync();
  }
}
