using OrderService.Web.Endpoints.Records;

namespace OrderService.Web.Endpoints.ShipperEndpoints;

public class AcceptProductIssuePaymentAndFinishResponse
{
  public ProductIssueShippingRecord productIssueShippingRecord { get; set; }

  public AcceptProductIssuePaymentAndFinishResponse(ProductIssueShippingRecord productIssueShippingRecord)
  {
    this.productIssueShippingRecord = productIssueShippingRecord;
  }
}
