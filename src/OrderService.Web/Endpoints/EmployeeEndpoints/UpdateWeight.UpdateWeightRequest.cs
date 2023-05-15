using System.ComponentModel.DataAnnotations;
using OrderService.Web.Validations;

namespace OrderService.Web.Endpoints.EmployeeEndpoints;

public class UpdateWeightRequest
{
  public const string Route = "/employee/orders/weight/update";

  [Required]
  public float weight { get; set; }

  [Required]
  public int productId { get; set; }
  [Required]
  public int orderId { get; set; }
}
