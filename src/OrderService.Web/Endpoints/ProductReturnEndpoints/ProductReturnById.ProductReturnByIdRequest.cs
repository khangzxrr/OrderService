namespace OrderService.Web.Endpoints.ProductReturnEndpoints;

public class ProductReturnByIdRequest
{
  public const string Route = "/productReturn";

  public int id { get; set; } 

}
