namespace OrderService.Web.Endpoints.OrderEndpoints;

public class GetByIdRequest
{
  public const string Route = "/orders/{OrderId:int}";

  public static string BuildRoute(int orderId) => Route.Replace("{OrderId:int}", orderId.ToString());

  public int OrderId { get; set; }
}
