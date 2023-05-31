using Ardalis.SmartEnum;

namespace OrderService.Core.ProductReturnAggregate;
public class ProductIssueFinishStatus : SmartEnum<ProductIssueFinishStatus>
{

  public static ProductIssueFinishStatus onGoing = new(nameof(onGoing), 0);

  public static ProductIssueFinishStatus processed = new(nameof(processed), 1);

  public ProductIssueFinishStatus(string name, int value) : base(name, value)
  {
  }
}
