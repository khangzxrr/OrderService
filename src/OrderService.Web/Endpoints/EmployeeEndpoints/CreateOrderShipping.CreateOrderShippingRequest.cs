using System.ComponentModel.DataAnnotations;

namespace OrderService.Web.Endpoints.EmployeeEndpoints;

public class CreateOrderShippingRequest
{
  public const string Route = "/employee/ordershipping/create";

  [Required]
  public int orderId { get; set; }
  [Required]  
  public bool isUsing3rd { get; set; }
  public string shippingDescription { get; set; } = "";


}
