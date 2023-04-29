using System.ComponentModel.DataAnnotations;

namespace OrderService.Web.Endpoints.ShipperEndpoints;

public class SetOrderShippingStatusRequest
{
  public const string Route = "/shipper/orders/updateShippingStatus";

  [Required]
  public string shippingStatus { get; set; }
  [Required]
  public int orderShippingId { get; set; }

  public SetOrderShippingStatusRequest(string shippingStatus, int orderShippingId)
  {
    this.shippingStatus = shippingStatus;
    this.orderShippingId = orderShippingId;
  }

}
