namespace OrderService.Web.Endpoints.ProductIssueEndpoints;

public class IsExistActiveProductIssueResponse
{
  public bool isExist { get; set; }
  public int productIssueId { get; set; }

  public IsExistActiveProductIssueResponse(bool isExist, int productIssueId)
  {
    this.isExist = isExist;
    this.productIssueId = productIssueId;
  }
}
