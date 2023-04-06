using Ardalis.Specification;

namespace OrderService.Core.OrderAggregate.Specifications;
public class OrderByEmployeeIdSpec : Specification<Order>
{
  public OrderByEmployeeIdSpec(int employeeId)
  {
    Query
      .Include(o => o.chat)
      .Where(o => o.chat.employee.Id == employeeId);
  }
}
