using Ardalis.SmartEnum;

namespace OrderService.Core.OrderAggregate;
public class OrderStatus : SmartEnum<OrderStatus>
{
  public static readonly OrderStatus noPayYet = new(nameof(noPayYet), 0);
  public static readonly OrderStatus firstPayment = new(nameof(firstPayment), 0);
  public static readonly OrderStatus deliverFromUsToVN = new(nameof(deliverFromUsToVN), 0);
  public static readonly OrderStatus deliverToCustomer = new(nameof(deliverToCustomer), 0);
  public static readonly OrderStatus finishPayment = new(nameof(finishPayment), 0);

  public OrderStatus(string name, int value): base(name, value) { }
}


