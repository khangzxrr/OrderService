﻿using Ardalis.SmartEnum;

namespace OrderService.Core.OrderShippingAggregate;
public class OrderShippingStatus : SmartEnum<OrderShippingStatus>
{
  public static readonly OrderShippingStatus inWarehouse = new(nameof(inWarehouse), 0);
  public static readonly OrderShippingStatus shipperTaken = new(nameof(shipperTaken), 1);
  public static readonly OrderShippingStatus shipping = new(nameof(shipping), 2);
  public static readonly OrderShippingStatus customerReceived = new(nameof(customerReceived), 3);

  public OrderShippingStatus(string name, int value) : base(name, value) { }
}
