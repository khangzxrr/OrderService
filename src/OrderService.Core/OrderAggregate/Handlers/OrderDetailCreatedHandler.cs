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

    var currencySpec = new CurrencyExchangeByName("US");
    var currency = await _currencyExchangeRepository.FirstOrDefaultAsync(currencySpec);
    if (currency == null)
    {
      throw new NullReferenceException("currency is null");
    }

    foreach (var orderDetail in order!.orderDetails)
    {
      totalCost += orderDetail.shipCost;
      totalCost += orderDetail.productCost * orderDetail.quantity;
      totalCost += orderDetail.additionalCost;
      totalCost += orderDetail.processCost;
    }

    totalCost = Math.Ceiling(totalCost * currency!.rate);

    order.SetPrice(totalCost);

    await _repository.SaveChangesAsync();
  }
}
