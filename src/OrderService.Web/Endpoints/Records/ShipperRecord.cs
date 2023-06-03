using OrderService.Core.ShipperAggregate;

namespace OrderService.Web.Endpoints.Records;

public record ShipperRecord(int id, string name, string status, int totalOrderShippings)
{
  public static ShipperRecord FromEntity(Shipper shipper)
  {
    return new ShipperRecord(shipper.Id, shipper.user.fullName, shipper.shippingStatus.Name, shipper.OrderShippings.Count);
  }
}
