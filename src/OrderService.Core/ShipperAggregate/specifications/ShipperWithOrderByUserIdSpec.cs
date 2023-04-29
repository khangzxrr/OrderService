using Ardalis.Specification;

namespace OrderService.Core.ShipperAggregate.specifications;
public class ShipperWithOrderByUserIdSpec : Specification<Shipper>, ISingleResultSpecification
{

  public ShipperWithOrderByUserIdSpec(int userId)
  {
    Query
      .Where(s => s.userId == userId)
      .Include(s => s.OrderShippings)
        .ThenInclude(os => os.order)
          .ThenInclude(o => o.user);
  }
}
