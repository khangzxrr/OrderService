using MediatR;
using OrderService.Core.OrderAggregate.Events;
using OrderService.Core.OrderAggregate.Specifications;
using OrderService.SharedKernel.Interfaces;

namespace OrderService.Core.OrderAggregate.Handlers;
public class OrderCreatedHandler : INotificationHandler<OrderCreatedEvent>
{
  private readonly IRepository<Order> _repository;

  public OrderCreatedHandler(IRepository<Order> repository)
  {
    _repository = repository;
  }

  public async Task Handle(OrderCreatedEvent notification, CancellationToken cancellationToken)
  {
    var spec = new OrderById(notification.OrderId);
    var order = await _repository.FirstOrDefaultAsync(spec);

    var totalCost = 0.0f;

    foreach(var orderDetail in order!.orderDetails)
    {
      totalCost += orderDetail.shipCost;
      totalCost += orderDetail.productCost * orderDetail.quantity;
      totalCost += orderDetail.additionalCost;
      totalCost += orderDetail.processCost;
    }

    order.price = totalCost;
    await _repository.SaveChangesAsync();
  }
}
