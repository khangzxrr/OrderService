namespace OrderService.Web.Endpoints.OrderEndpoints;

public class GetChatRequest
{
  public const string Route = "/orders/{orderId:int}/chats";

  public static string BuildRoute(int orderId) => Route.Replace("{orderId:int}", orderId.ToString());


  public int orderId { get; set; }
}
