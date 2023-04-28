using Ardalis.Specification;

namespace OrderService.Core.ShipperAggregate.specifications;
public class ShipperWithOrderByShipperIdSpec : Specification<Shipper>, ISingleResultSpecification
{

  public ShipperWithOrderByShipperIdSpec(int shipperId)
  {
    Query
      .Where(s => s.Id == shipperId)
      .Include(s => s.OrderShippings)
        .ThenInclude(os => os.order)
          .ThenInclude(o => o.user);
  }
}
