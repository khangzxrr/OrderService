using Ardalis.SmartEnum;

namespace OrderService.Core.ProductReturnAggregate;
public class ProductReturnStatus : SmartEnum<ProductReturnStatus>
{
  public static ProductReturnStatus request = new(nameof(request), 0);

  public static ProductReturnStatus acceptReturnEmployeeFault = new(nameof(acceptReturnEmployeeFault), 2);
  public static ProductReturnStatus acceptReturnCustomerFault = new(nameof(acceptReturnEmployeeFault), 3);
  public static ProductReturnStatus acceptReturnSellerFault = new(nameof(acceptReturnSellerFault), 4);

  public ProductReturnStatus(string name, int value) : base(name, value)
  {
  }
}
