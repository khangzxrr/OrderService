using MediatR;
using OrderService.Core.CurrencyAggregate;
using OrderService.Core.CurrencyAggregate.Specifications;
using OrderService.Core.OrderAggregate.Events;
using OrderService.Core.OrderAggregate.Specifications;
using OrderService.SharedKernel.Interfaces;

namespace OrderService.Core.OrderAggregate.Handlers;
public class OrderDetailCreatedHandler : INotificationHandler<OrderDetailCreatedEvent>
{
  private readonly IRepository<Order> _repository;
  private readonly IRepository<CurrencyExchange> _currencyExchangeRepository;
  public OrderDetailCreatedHandler(IRepository<Order> repository, IRepository<CurrencyExchange> currencyExchangeRepository)
  {
    _repository = repository;
    _currencyExchangeRepository = currencyExchangeRepository;
  }

  public async Task Handle(OrderDetailCreatedEvent notification, CancellationToken cancellationToken)
  {
    var spec = new OrderByIdSpec(notification.OrderId);
    var order = await _repository.FirstOrDefaultAsync(spec);

    double totalCost = 0.0f;

    float totalCostOfOrderDetail;

    foreach (var orderDetail in order!.orderDetails)
    {
      totalCostOfOrderDetail = orderDetail.shipCost;
      totalCostOfOrderDetail += orderDetail.product.productPrice * orderDetail.quantity;
      totalCostOfOrderDetail += orderDetail.additionalCost;
      totalCostOfOrderDetail += orderDetail.processCost;

      orderDetail.setTotalCost(totalCostOfOrderDetail);

      totalCost += totalCostOfOrderDetail * orderDetail.product.currencyExchange.rate;
    }

    totalCost = Math.Ceiling(totalCost);

    order.SetPrice(totalCost);

    await _repository.SaveChangesAsync();
  }
}
