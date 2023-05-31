
using Ardalis.Specification;

namespace OrderService.Core.OrderAggregate.Specifications;
public class OrderByOrderDetailIdSpec : Specification<Order>
{
  public OrderByOrderDetailIdSpec(int orderDetailId)
  {
    Query
      .Include(o => o.orderDetails.Where(od => od.Id == orderDetailId))
        .ThenInclude(od => od.product)
          .ThenInclude(p => p.productCategory)
      .Include(o => o.orderDetails.Where(od => od.Id == orderDetailId))
        .ThenInclude(od => od.product)
          .ThenInclude(p => p.currencyExchange)
            .ThenInclude(p => p.currency)

      .Include(o => o.chat)
        .ThenInclude(c => c.employee)
      .Include(o => o.user);




  }
}
