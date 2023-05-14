using System.ComponentModel.DataAnnotations;

namespace OrderService.Web.Endpoints.EmployeeEndpoints;

public class MarkUserTakenOrderShippingRequest
{
  public const string Route = "/employee/orders/ordershipping/usertaken/finish";

  [Required]
  public int orderId { get; set; }
}
