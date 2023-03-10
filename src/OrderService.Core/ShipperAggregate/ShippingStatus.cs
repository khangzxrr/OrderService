using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ardalis.SmartEnum;

namespace OrderService.Core.ShipperAggregate;
public class ShippingStatus : SmartEnum<ShippingStatus>
{
  public readonly ShippingStatus online = new(nameof(online), 1);
  public readonly ShippingStatus offline = new(nameof(offline), 0);

  public ShippingStatus(string name, int value) : base(name, value) { }
}
