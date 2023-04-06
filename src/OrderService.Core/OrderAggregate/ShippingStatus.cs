using Ardalis.SmartEnum;

namespace OrderService.Core.OrderAggregate;
public class ShippingStatus : SmartEnum<ShippingStatus>
{
  public static readonly ShippingStatus inVNwarehouse = new(nameof(inVNwarehouse), 0);
  public static readonly ShippingStatus shippingToCustomer = new(nameof(shippingToCustomer), 1);

  public ShippingStatus(string name, int value) : base(name, value) { }
}
