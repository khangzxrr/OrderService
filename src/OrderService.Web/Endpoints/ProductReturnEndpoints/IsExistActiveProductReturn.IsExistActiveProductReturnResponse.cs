namespace OrderService.Web.Endpoints.ProductReturnEndpoints;

public class IsExistActiveProductReturnResponse
{
  public bool isExist { get; set; }
  public int productReturnId { get; set; }

  public IsExistActiveProductReturnResponse(bool isExist, int productReturnId)
  {
    this.isExist = isExist;
    this.productReturnId = productReturnId;
  }
}
