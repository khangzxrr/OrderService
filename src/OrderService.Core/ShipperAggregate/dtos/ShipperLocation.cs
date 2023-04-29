namespace OrderService.Core.ShipperAggregate.dtos;
public class ShipperLocation
{
  public string shipperHash { get; set; }
  public double latitule { get; }
  public double longitude { get; }

  public ShipperLocation(int userId, double latitule, double longitude)
  {
    this.shipperHash = $"shipper_{userId}";
    this.latitule = latitule;
    this.longitude = longitude;
  }
}
