using System.Data.Common;
using Ardalis.SmartEnum;

namespace OrderService.Core.ShipperAggregate.enums;
public class ShipperPayingMethod: SmartEnum<ShipperPayingMethod>
{
  public static readonly ShipperPayingMethod byCash = new(nameof(byCash), 0);
  public static readonly ShipperPayingMethod byOnline = new(nameof(byOnline), 1);
  
  public ShipperPayingMethod(string name, int value) : base(name, value) { }
}
