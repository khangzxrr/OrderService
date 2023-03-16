using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ardalis.Specification;

namespace OrderService.Core.OrderAggregate.Specifications;
public class OrderById : Specification<Order>, ISingleResultSpecification
{
  public OrderById(int id)
  {
    Query
      .Where(o => o.Id == id)
      .Include(o => o.chat)
      .Include(o => o.orderDetails)
      .Include(o => o.orderPayments);
  }
}
