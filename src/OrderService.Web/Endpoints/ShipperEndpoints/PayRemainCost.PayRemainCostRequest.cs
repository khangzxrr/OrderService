using System.ComponentModel.DataAnnotations;

namespace OrderService.Web.Endpoints.ShipperEndpoints;

public class PayRemainCostRequest
{
  public const string Route = "/shipper/orders/remainCost/confirm";

  [Required]
  public int orderId { get; set; }
  [Required]
  public int orderShippingId { get; set; }
  [Required]
  public string payMethod { get; set; }

  public PayRemainCostRequest(int orderId, int orderShippingId, string payMethod)
  {
    this.orderId = orderId;
    this.orderShippingId = orderShippingId;
    this.payMethod = payMethod;
  }
}
