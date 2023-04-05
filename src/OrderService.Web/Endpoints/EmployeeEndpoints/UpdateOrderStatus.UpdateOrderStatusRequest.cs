using System.ComponentModel.DataAnnotations;
using OrderService.Core.OrderAggregate;

namespace OrderService.Web.Endpoints.EmployeeEndpoints;

public class UpdateOrderStatusRequest
{
  public const string Route = "/employee/order/updateStatus";

  [Required]
  public int orderId { get; set; }

  [Required]
  public string orderStatus { get; set; }


  
  public UpdateOrderStatusRequest(int orderId, string orderStatus)
  {
    this.orderStatus = orderStatus;
    this.orderId = orderId;
  }
}
