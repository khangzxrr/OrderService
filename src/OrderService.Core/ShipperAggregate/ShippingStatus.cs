using Ardalis.SmartEnum;

namespace OrderService.Core.ShipperAggregate;
public class ShippingStatus : SmartEnum<ShippingStatus>
{
  public static readonly ShippingStatus busy = new(nameof(busy), 2);
  public static readonly ShippingStatus online = new(nameof(online), 1);
  public static readonly ShippingStatus offline = new(nameof(offline), 0);

  public ShippingStatus(string name, int value) : base(name, value) { }
}
