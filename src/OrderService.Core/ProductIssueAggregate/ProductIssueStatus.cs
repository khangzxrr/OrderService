using Ardalis.SmartEnum;

namespace OrderService.Core.ProductReturnAggregate;
public class ProductIssueStatus : SmartEnum<ProductIssueStatus>
{
  public static ProductIssueStatus request = new(nameof(request), 0);

  public static ProductIssueStatus acceptEmployeeFault = new(nameof(acceptEmployeeFault), 2);
  public static ProductIssueStatus acceptCustomerFault = new(nameof(acceptCustomerFault), 3);
  public static ProductIssueStatus acceptSellerFault = new(nameof(acceptSellerFault), 4);

  public static ProductIssueStatus exchangeforNew = new(nameof(exchangeforNew), 6);

  public static ProductIssueStatus sentToSeller = new(nameof(sentToSeller), 7);

  public static ProductIssueStatus successExchangeReturnToVN = new(nameof(successExchangeReturnToVN), 8);

  public static ProductIssueStatus failedExchangeSellerRejectReturnToVN = new(nameof(failedExchangeSellerRejectReturnToVN), 9);


  public static ProductIssueStatus refund = new(nameof(refund), 5);


  public static ProductIssueStatus shippingToCustomer = new(nameof(shippingToCustomer), 10);

  public static ProductIssueStatus shipperReceived = new(nameof(shipperReceived), 11);

  public static ProductIssueStatus customerReceived = new(nameof(customerReceived), 12);


  public static ProductIssueStatus finish = new(nameof(finish), 99);



  public ProductIssueStatus(string name, int value) : base(name, value)
  {    
  }
}
