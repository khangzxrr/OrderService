using System.ComponentModel.DataAnnotations;

namespace OrderService.Web.Endpoints.OrderEndpoints;

public class CreateOrderRequest
{
  public const string Route = "/Orders";

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

  [Required]
  public List<OrderProductRecord> products { get; set; } = new List<OrderProductRecord>();

  public string customerDescription { get; set; }

  [Required]
  public string address { get; set; }

  [Required]
  public string phoneNumber { get; set; } 
}
