
using Ardalis.Specification;

namespace OrderService.Core.OrderAggregate.Specifications;
public class OrderChatByIdSpec : Specification<Order>, ISingleResultSpecification
{
  public OrderChatByIdSpec(int orderId)
  {
    Query
      .Where(o => o.Id == orderId)
      .Include(o => o.chat)
        .ThenInclude(c => c.chatMessages);
  }
}
