using OrderService.Web.Endpoints.Records;

namespace OrderService.Web.Endpoints.ProductIssueEndpoints;

public class ProductIssueByIdResponse
{
  public ProductIssueRecord productIssueRecord { get; set; }

  public ProductIssueByIdResponse(ProductIssueRecord productIssueRecord)
  {
    this.productIssueRecord = productIssueRecord;
  }
}
