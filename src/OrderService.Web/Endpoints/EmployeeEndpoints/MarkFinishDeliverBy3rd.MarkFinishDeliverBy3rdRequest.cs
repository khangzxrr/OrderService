using System.ComponentModel.DataAnnotations;

namespace OrderService.Web.Endpoints.EmployeeEndpoints;

public class MarkFinishDeliverBy3rdRequest
{
  public const string Route = "/employee/orders/ordershipping/3rd/finish";

  [Required]
  public int orderId { get; set; }

}
