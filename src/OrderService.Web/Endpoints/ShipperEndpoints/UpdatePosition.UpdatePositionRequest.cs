using System.ComponentModel.DataAnnotations;

namespace OrderService.Web.Endpoints.ShipperEndpoints;

public class UpdatePositionRequest
{
  public const string Route = "/shipper/position/update";

  [Required]
  public double latitule { get; set; }
  [Required] 
  public double longitude { get; set; }
}
