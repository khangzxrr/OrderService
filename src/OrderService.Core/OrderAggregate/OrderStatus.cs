using Ardalis.SmartEnum;

namespace OrderService.Core.OrderAggregate;
public class OrderStatus : SmartEnum<OrderStatus>
{
  public static readonly OrderStatus noPriceQuotation = new(nameof(noPriceQuotation), 0);

  public static readonly OrderStatus noPayYet = new(nameof(noPayYet), 1);
  public static readonly OrderStatus waitingToOrderFromSeller = new(nameof(waitingToOrderFromSeller), 2);
  public static readonly OrderStatus orderingFromSeller = new(nameof(orderingFromSeller), 3);
  public static readonly OrderStatus deliverFromUsToVN = new(nameof(deliverFromUsToVN), 4);
  public static readonly OrderStatus inVNwarehouse = new(nameof(inVNwarehouse), 5);
  public static readonly OrderStatus deliverToCustomer = new(nameof(deliverToCustomer), 6);
  public static readonly OrderStatus finished = new(nameof(finished), 7);

  public OrderStatus(string name, int value): base(name, value) { }
}


