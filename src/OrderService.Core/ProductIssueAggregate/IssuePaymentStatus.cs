
using Ardalis.SmartEnum;

namespace OrderService.Core.ProductReturnAggregate;
public class IssuePaymentStatus : SmartEnum<IssuePaymentStatus>
{

  public static IssuePaymentStatus customerPay = new(nameof(customerPay), 0);

  public static IssuePaymentStatus refund_100 = new(nameof(refund_100), 1);

  public static IssuePaymentStatus refund_50 = new(nameof(refund_50), 2);


  public IssuePaymentStatus(string name, int value) : base(name, value)
  {
  }
}
