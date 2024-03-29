﻿using Ardalis.Result;
using OrderService.Core.OrderPaymentAggregate;

namespace OrderService.Core.Interfaces;
public interface IOrderPaymentService
{
  public Task<Result<OrderPayment>> AddNewPayment(int orderId, string paymentTurn, long amount, string transactionId);
}
