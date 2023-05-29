
using Ardalis.SmartEnum;

namespace OrderService.Core.ProductReturnAggregate;
public class ReturnPaymentStatus : SmartEnum<ReturnPaymentStatus>
{

  public static ReturnPaymentStatus customerPay = new(nameof(customerPay), 0);

  public static ReturnPaymentStatus refund_100 = new(nameof(refund_100), 1);

  public static ReturnPaymentStatus refund_50 = new(nameof(refund_50), 2);


  public ReturnPaymentStatus(string name, int value) : base(name, value)
  {
  }
}
