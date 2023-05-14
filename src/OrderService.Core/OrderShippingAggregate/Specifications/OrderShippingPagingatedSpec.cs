

using Ardalis.Specification;

namespace OrderService.Core.OrderShippingAggregate.Specifications;
public class OrderShippingPagingatedSpec : Specification<OrderShipping>
{
  public OrderShippingPagingatedSpec(int skip, int take)
  {
    if (take == 0)
    {
      take = int.MaxValue;
    }

    Query
      .Include(os => os.shipper)
        .ThenInclude(s => s!.user)
      .Skip(skip) 
      .Take(take);
  }
}
