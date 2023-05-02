using Ardalis.SmartEnum;

namespace OrderService.Core.OrderAggregate;
public class OrderLocalShippingStatus : SmartEnum<OrderLocalShippingStatus>
{
  public static readonly OrderLocalShippingStatus notInQueue = new(nameof(notInQueue), 0);

  public static readonly OrderLocalShippingStatus inQueue = new( nameof(inQueue), 1);

  public static readonly OrderLocalShippingStatus assignedShipper = new(nameof(assignedShipper), 2);

  public static readonly OrderLocalShippingStatus delivered = new(nameof(delivered), 3);

  public OrderLocalShippingStatus(string name, int value) : base(name, value) { }
}
