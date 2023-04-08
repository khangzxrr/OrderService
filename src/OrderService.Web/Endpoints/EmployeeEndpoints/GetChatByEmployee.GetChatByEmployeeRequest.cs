namespace OrderService.Web.Endpoints.EmployeeEndpoints;

public class GetChatByEmployeeRequest
{
  public const string Route = "/employee/orders/{orderId:int}/chats";

  public static string BuildRoute(int orderId) => Route.Replace("{orderId:int}", orderId.ToString());

  public int orderId { get; set; }
}
