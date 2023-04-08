using Ardalis.Specification;

namespace OrderService.Core.OrderAggregate.Specifications;
public class OrderChatByIdAndEmployeeIdSpec : Specification<Order>
{
  public OrderChatByIdAndEmployeeIdSpec(int orderId, int employeeId)
  {
    Query
      .Where(o => o.Id == orderId)
      .Include(o => o.chat)
        .ThenInclude(c => c.employee)
      .Include(o => o.chat)
        .ThenInclude(c => c.chatMessages);
      
  }
}
