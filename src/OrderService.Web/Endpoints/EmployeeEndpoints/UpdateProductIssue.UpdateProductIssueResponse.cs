using OrderService.Web.Endpoints.Records;

namespace OrderService.Web.Endpoints.EmployeeEndpoints;

public class UpdateProductIssueResponse
{
  public ProductIssueRecord productIssueRecord { get; set; }

  public UpdateProductIssueResponse(ProductIssueRecord productIssueRecord)
  {
    this.productIssueRecord = productIssueRecord;
  }
}
