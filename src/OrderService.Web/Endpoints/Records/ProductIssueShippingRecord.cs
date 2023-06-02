using OrderService.Core.ProductReturnAggregate;

namespace OrderService.Web.Endpoints.Records;

public record ProductIssueShippingRecord(int id, string productName, string customerFullname, string customerPhonenumber, string customerAddress, string shippingStatus , float customerPayAmount)
{
  public static ProductIssueShippingRecord FromEntity(ProductIssue productIssue)
  {

    float customerPayAmount = productIssue.issuePayments.Where(p => p.paymentStatus == IssuePaymentStatus.customerPay).Sum(p => p.cost);

    return new ProductIssueShippingRecord(productIssue.Id, productIssue.product.productName, productIssue.customerFullname, productIssue.customerPhonenumber, productIssue.customerAddress, productIssue.productIssueShipping!.shippingStatus.Name, customerPayAmount);
  }
}
