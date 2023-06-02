using OrderService.Web.Endpoints.Records;

namespace OrderService.Web.Endpoints.ShipperEndpoints;

public class GetProductIssueShippingsResponse
{
  public IEnumerable<ProductIssueShippingRecord> productIssueShippingRecords { get; set; }

  public GetProductIssueShippingsResponse(IEnumerable<ProductIssueShippingRecord> productIssueShippingRecords)
  {
    this.productIssueShippingRecords = productIssueShippingRecords;
  }
}
