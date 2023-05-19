using Ardalis.Specification;

namespace OrderService.Core.ShipperAggregate.specifications;
public class ShipperPaginatedSpec : Specification<Shipper>
{
  public ShipperPaginatedSpec(int skip, int take)
  {
    Query
      .Include(s => s.user)
      .Skip(skip)
      .Take(take);
  }
}
