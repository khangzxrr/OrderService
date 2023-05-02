using System.ComponentModel.DataAnnotations;
using OrderService.Web.Validations;

namespace OrderService.Web.Endpoints.EmployeeEndpoints;

public class UpdateWeightRequest
{
  public const string Route = "/employee/orders/weight/update";

  [Required]
  [NotZero]
  public double weight { get; set; }

  [Required]
  public int orderId { get; set; }
}
