namespace OrderService.Web.Endpoints.EmployeeEndpoints;

public class GetOrderByEmployeeRequest
{
  public const string Route = "/employee/Orders/{orderId:int}";

  public static string BuildRoute(int orderId) => Route.Replace("{orderId:int}", orderId.ToString());

  public int orderId { get; set; }


}
