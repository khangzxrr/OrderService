

using Ardalis.Specification;
using OrderService.Core.ProductReturnAggregate;

namespace OrderService.Core.ProductIssueAggregate.specifications;
public class ProductIssuePagingatedFilterByEmployeeIdSpec : Specification<ProductIssue>
{
  public ProductIssuePagingatedFilterByEmployeeIdSpec(int take, int skip, int employeeId)
  {
    Query
      .Include(pi => pi.assignedEmployee)
      .Where(pi => pi.assignedEmployee.Id == employeeId)

      .Include(pi => pi.product)
        .ThenInclude(p => p.productCategory)
      .Include(pi => pi.product)
        .ThenInclude(p => p.currencyExchange)
          .ThenInclude(ce => ce.currency)
      .Take(take)
      .Skip(skip);
  }
}
