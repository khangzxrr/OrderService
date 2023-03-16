namespace OrderService.Web.Endpoints.OrderEndpoints;

public class PayRequest
{
  public const string Route = "/orders/{OrderId:int}/pay";
  public static string BuildRoute(int OrderId) => Route.Replace("{OrderId:int}", OrderId.ToString());

  public int OrderId { get; set; }
}
