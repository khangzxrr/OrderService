
using Ardalis.Specification;

namespace OrderService.Core.OrderAggregate.Specifications;
public class OrderOrderDetailSpec : Specification<Order>
{
  public OrderOrderDetailSpec()
  {
    Query
      .Include(o => o.orderDetails)
        .ThenInclude(od => od.product)
            .ThenInclude(p => p.productCategory)
      .Include(o => o.orderDetails)
         .ThenInclude(od => od.product)
            .ThenInclude(p => p.currencyExchange)
              .ThenInclude(p => p.currency)

      .Include(o => o.chat)
        .ThenInclude(c => c.employee)
      .Include(o => o.user);
      




  }
}
