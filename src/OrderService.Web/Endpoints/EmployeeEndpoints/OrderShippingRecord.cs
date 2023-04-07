﻿using OrderService.Core.OrderShippingAggregate;
using OrderService.Core.ShipperAggregate;

namespace OrderService.Web.Endpoints.EmployeeEndpoints;

public record OrderShippingRecord(int id, bool shippingUsing3rd, string shippingStatus, string shippingDescription, string signatureImageUrl, string shipperName)
{
  public static OrderShippingRecord FromEntity(OrderShipping orderShipping)
  {
    return new OrderShippingRecord(orderShipping.Id,
        orderShipping.shippingUsing3rd,
        orderShipping.orderShippingStatus.Name,
        orderShipping.shippingDescription,
        orderShipping.signatureImageUrl,
        (orderShipping.shippingUsing3rd) ? "" : orderShipping.shipper.userId.ToString()
      );
  }
}