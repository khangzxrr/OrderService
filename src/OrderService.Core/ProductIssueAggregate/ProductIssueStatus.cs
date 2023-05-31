using Ardalis.SmartEnum;

namespace OrderService.Core.ProductReturnAggregate;
public class ProductIssueStatus : SmartEnum<ProductIssueStatus>
{
  public static ProductIssueStatus request = new(nameof(request), 0);

  public static ProductIssueStatus acceptReturnEmployeeFault = new(nameof(acceptReturnEmployeeFault), 2);
  public static ProductIssueStatus acceptReturnCustomerFault = new(nameof(acceptReturnCustomerFault), 3);
  public static ProductIssueStatus acceptReturnSellerFault = new(nameof(acceptReturnSellerFault), 4);

  public ProductIssueStatus(string name, int value) : base(name, value)
  {
  }
}
