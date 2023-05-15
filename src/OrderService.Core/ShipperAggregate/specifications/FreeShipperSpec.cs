using Ardalis.Specification;

namespace OrderService.Core.ShipperAggregate.specifications;
public class FreeShipperSpec : Specification<Shipper>, ISingleResultSpecification
{
  public FreeShipperSpec()
  {
    Query
      .Include(s => s.user)
      .Where(s => s.shippingStatus == ShippingStatus.online);
  }
}
