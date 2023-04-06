using Ardalis.Specification;

namespace OrderService.Core.ShipperAggregate.specifications;
public class FreeShipperSpec : Specification<Shipper>, ISingleResultSpecification
{
  public FreeShipperSpec()
  {
    Query
      .Where(s => s.shippingStatus == ShippingStatus.online);
  }
}
