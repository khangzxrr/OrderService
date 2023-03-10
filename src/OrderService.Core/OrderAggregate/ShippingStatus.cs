using Ardalis.SmartEnum;

namespace OrderService.Core.OrderAggregate;
public class ShippingStatus : SmartEnum<ShippingStatus>
{
  public static readonly ShippingStatus inUSwarehouse = new(nameof(inUSwarehouse), 0);
  public static readonly ShippingStatus shippingFromUsToVn = new(nameof(shippingFromUsToVn), 1);
  public static readonly ShippingStatus inVNwarehouse = new(nameof(inVNwarehouse), 2);
  public static readonly ShippingStatus shippingToCustomer= new(nameof(shippingToCustomer), 3);

  public ShippingStatus(string name, int value) : base(name, value) { }
}
