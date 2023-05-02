using System.ComponentModel.DataAnnotations;

namespace OrderService.Web.Endpoints.ShipperEndpoints;

public class PayRemainCostRequest
{
  public const string Route = "/shipper/orders/remainCost/confirm";

  [Required]
  public int orderId { get; set; }
  [Required]
  public string payMethod { get; set; }

  public PayRemainCostRequest(int orderId, string payMethod)
  {
    this.orderId = orderId;
    this.payMethod = payMethod;
  }
}
