using System.ComponentModel.DataAnnotations;

namespace OrderService.Web.Endpoints.OrderEndpoints;

public class CreateOrderRequest
{
  public const string Route = "/Orders";

  public List<OrderProductRecord> products { get; set; } = new List<OrderProductRecord>();
}
