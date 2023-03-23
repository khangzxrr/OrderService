﻿using MediatR;
using OrderService.Core.CurrencyAggregate;
using OrderService.Core.CurrencyAggregate.Specifications;
using OrderService.Core.OrderAggregate.Events;
using OrderService.Core.OrderAggregate.Specifications;
using OrderService.SharedKernel.Interfaces;

namespace OrderService.Core.OrderAggregate.Handlers;
public class OderDetailCreatedHandler : INotificationHandler<OrderDetailCreatedEvent>
{
  private readonly IRepository<Order> _repository;
  private readonly IRepository<CurrencyExchange> _currencyExchangeRepository;
  public OderDetailCreatedHandler(IRepository<Order> repository, IRepository<CurrencyExchange> currencyExchangeRepository)
  {
    _repository = repository;
    _currencyExchangeRepository = currencyExchangeRepository;
  }

  public async Task Handle(OrderDetailCreatedEvent notification, CancellationToken cancellationToken)
  {
    var spec = new OrderByIdSpec(notification.OrderId);
    var order = await _repository.FirstOrDefaultAsync(spec);

    var totalCost = 0.0f;

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

    order.price = totalCost * currency!.rate;
    await _repository.SaveChangesAsync();
  }
}
