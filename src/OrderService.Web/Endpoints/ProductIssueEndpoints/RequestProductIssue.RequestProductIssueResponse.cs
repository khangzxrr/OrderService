using OrderService.Web.Endpoints.Records;

namespace OrderService.Web.Endpoints.ProductIssueEndpoints;

public class RequestProductIssueResponse
{
  public ProductIssueRecord productIssueRecord { get; set; }

  public RequestProductIssueResponse(ProductIssueRecord productIssueRecord)
  {
    this.productIssueRecord = productIssueRecord;
  }
}
