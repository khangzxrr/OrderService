namespace OrderService.Web.Endpoints.ShipperEndpoints;

public class GetAllOrderResponse
{
  public IEnumerable<ShipperOrderRecord> shipperOrderRecords { get; set; }

  public GetAllOrderResponse(IEnumerable<ShipperOrderRecord> shipperOrderRecords)
  {
    this.shipperOrderRecords = shipperOrderRecords;
  }
}
