using System.ComponentModel.DataAnnotations;

namespace OrderService.Web.Endpoints.EmployeeEndpoints;

public class UpdateOrderAddressRequest
{
  public const string Route = "/employee/orders/address/update";

  [Required]
  public int orderId { get; set; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

  [Required]
  public string address { get; set; }
}
