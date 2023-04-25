using Ardalis.SmartEnum;

namespace OrderService.Core.OrderAggregate;
public class OrderShippingStatus : SmartEnum<OrderShippingStatus>
{
  public static readonly OrderShippingStatus notInQueue = new(nameof(notInQueue), 0);

  public static readonly OrderShippingStatus inQueue = new( nameof(inQueue), 1);

  public static readonly OrderShippingStatus assignedShipper = new(nameof(assignedShipper), 2);

  public OrderShippingStatus(string name, int value) : base(name, value) { }
}
