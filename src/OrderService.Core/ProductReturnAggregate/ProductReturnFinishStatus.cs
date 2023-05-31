using Ardalis.SmartEnum;

namespace OrderService.Core.ProductReturnAggregate;
public class ProductReturnFinishStatus : SmartEnum<ProductReturnFinishStatus>
{

  public static ProductReturnFinishStatus onGoing = new(nameof(onGoing), 0);

  public static ProductReturnFinishStatus processed = new(nameof(processed), 1);

  public ProductReturnFinishStatus(string name, int value) : base(name, value)
  {
  }
}
