using Ardalis.SmartEnum;
using OrderService.SharedKernel;
using OrderService.SharedKernel.Interfaces;

namespace OrderService.Core.OrderShippingAggregate;
public class OrderShippingStatus : SmartEnum<OrderShippingStatus>
{
  public static readonly OrderShippingStatus inWarehouse = new(nameof(inWarehouse), 0);
  public static readonly OrderShippingStatus shipping = new(nameof(shipping), 1);
  public static readonly OrderShippingStatus customerReceived = new(nameof(customerReceived), 2);

  public OrderShippingStatus(string name, int value) : base(name, value) { }
}
