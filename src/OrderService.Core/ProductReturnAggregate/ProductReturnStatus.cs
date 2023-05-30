using Ardalis.SmartEnum;

namespace OrderService.Core.ProductReturnAggregate;
public class ProductReturnStatus : SmartEnum<ProductReturnStatus>
{
  public static ProductReturnStatus requestReturn = new(nameof(requestReturn), 0);
  public static ProductReturnStatus requestWarranty = new(nameof(requestWarranty), 1);


  public static ProductReturnStatus acceptReturnEmployeeFault = new(nameof(acceptReturnEmployeeFault), 2);
  public static ProductReturnStatus acceptReturnCustomerFault = new(nameof(acceptReturnEmployeeFault), 3);
  public static ProductReturnStatus acceptReturnSellerFault = new(nameof(acceptReturnSellerFault), 4);

  public static ProductReturnStatus acceptWarranty = new(nameof(acceptWarranty), 5);



  public ProductReturnStatus(string name, int value) : base(name, value)
  {
  }
}
