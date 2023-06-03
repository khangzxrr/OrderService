using OrderService.Core.ShipperAggregate;

namespace OrderService.Web.Endpoints.Records;

public record ShipperRecord(int id, string name, string shippingAddress, string status, int totalOrderShippings)
{
  public static ShipperRecord FromEntity(Shipper shipper)
  {
    return new ShipperRecord(shipper.Id, shipper.user.fullName, shipper.shippingDistrict, shipper.shippingStatus.Name, shipper.OrderShippings.Count);
  }
}
