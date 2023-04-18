using System.ComponentModel.DataAnnotations;

namespace OrderService.Web.Endpoints.OrderEndpoints;

public class SendMessageRequest
{
  public const string Route = "/orders/sendmessages";

  [Required]
  public int orderId { get; set; }

  [Required]
  public string message { get; set; }

  public SendMessageRequest(int orderId, string message)
  {
    this.orderId = orderId;
    this.message = message;
  }
}
