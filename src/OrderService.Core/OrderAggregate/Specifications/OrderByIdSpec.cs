using Ardalis.Specification;

namespace OrderService.Core.OrderAggregate.Specifications;
public class OrderByIdSpec : Specification<Order>, ISingleResultSpecification
{
  public OrderByIdSpec(int id)
  {
    Query
      .Where(o => o.Id == id)
      .Include(o => o.orderPayments)
      .Include(o => o.orderDetails)
        .ThenInclude(od => od.productHistory)
        .ThenInclude(ph => ph.productCategory)
        .ThenInclude(pc => pc.productShipCost);

  }
}
