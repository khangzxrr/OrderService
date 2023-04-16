﻿using OrderService.Core.OrderAggregate;
using OrderService.Web.Endpoints.ProductEndpoints;

namespace OrderService.Web.Endpoints.OrderEndpoints;

public record OrderDetailRecord(
  int orderDetailId, 
  float additionalCost, 
  float shipCost,
  float productCost,
  float processCost,
  int quantity,
  ProductRecord product
  )
{
  public static OrderDetailRecord FromEntity(OrderDetail orderDetail)
  {
    return new OrderDetailRecord(orderDetail.Id, orderDetail.additionalCost, orderDetail.shipCost, orderDetail.productCost, orderDetail.processCost, orderDetail.quantity, ProductRecord.FromEntity(orderDetail.product));
  }
}
