using Ardalis.Specification;

namespace OrderService.Core.OrderAggregate.Specifications;
public class OrderChatByIdAndUserIdSpec : Specification<Order>, ISingleResultSpecification
{
  public OrderChatByIdAndUserIdSpec(int orderId, int userId)
  {
    Query
      .Where(o => o.Id == orderId && o.userId == userId)
      .Include(o => o.chat)
        .ThenInclude(c => c.chatMessages);
  }
}
