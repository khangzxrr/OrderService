using System.ComponentModel.DataAnnotations;

namespace OrderService.Web.Endpoints.EmployeeEndpoints;

public class SendMessageToOrderRequest
{
  public const string Route = "/employee/orders/sendmessage";

  [Required]
  public int orderId { get; set; }
  [Required]
  public string message { get; set; }

  public SendMessageToOrderRequest(int orderId, string message)
  {
    this.orderId = orderId;
    this.message = message;
  }
}
