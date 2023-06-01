
using Ardalis.SmartEnum;

namespace OrderService.Core.ProductReturnAggregate;
public class IssuePaymentStatus : SmartEnum<IssuePaymentStatus>
{

  public static IssuePaymentStatus customerPay = new(nameof(customerPay), 0);

  public static IssuePaymentStatus refund = new(nameof(refund), 1);



  public IssuePaymentStatus(string name, int value) : base(name, value)
  {
  }
}
