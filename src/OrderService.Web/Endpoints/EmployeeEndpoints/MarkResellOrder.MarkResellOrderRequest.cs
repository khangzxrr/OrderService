using System.ComponentModel.DataAnnotations;

namespace OrderService.Web.Endpoints.EmployeeEndpoints;

public class MarkResellOrderRequest
{
  public const string Route = "/employee/orders/markResell";

  [Required]
  public int orderId { get; set; }


}
