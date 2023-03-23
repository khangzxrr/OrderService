using Ardalis.Specification;

namespace OrderService.Core.OrderAggregate.Specifications;
public class OrderPaymentChatByIdSpec : Specification<Order>, ISingleResultSpecification
{
  public OrderPaymentChatByIdSpec(int orderId)
  {
    Query
      .Where(o => o.Id == orderId)
      .Include(o => o.orderPayments)
      .Include(o => o.chat)
        .ThenInclude(c => c.chatMessages);  
  }
}
