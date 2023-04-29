namespace OrderService.Web.Endpoints.ShipperEndpoints;

public class SetOrderShippingStatusResponse
{
  public ShipperOrderRecord shipperOrderRecord { get; set; }

  public SetOrderShippingStatusResponse(ShipperOrderRecord shipperOrderRecord)
  {
    this.shipperOrderRecord = shipperOrderRecord;
  }
}
