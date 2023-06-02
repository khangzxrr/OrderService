using Ardalis.Specification;

namespace OrderService.Core.OrderAggregate.Specifications;
public class OrderByIdSpec : Specification<Order>, ISingleResultSpecification
{
  public OrderByIdSpec(int id)
  {
    Query
      .Where(o => o.Id == id)
      .Include(o => o.user)
      .Include(o => o.orderPayments)
      .Include(o => o.orderDetails)
        .ThenInclude(od => od.product)
        .ThenInclude(ph => ph.productCategory)
      .Include(o => o.orderDetails)
        .ThenInclude(od => od.product)
          .ThenInclude(p => p.currencyExchange)
            .ThenInclude(ce => ce.currency)
      .Include(o => o.chat)
        .ThenInclude(o => o.employee);
     

  }
}
