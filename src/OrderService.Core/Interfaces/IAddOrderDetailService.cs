﻿using Ardalis.Result;
using OrderService.Core.OrderAggregate;

namespace OrderService.Core.Interfaces;
public interface IAddOrderDetailService
{
  public Task<Result> AddOrderDetail(Order order, int productId, int quantity);
  public Task<Result> UpdateOrderDetails(Order order);
}
