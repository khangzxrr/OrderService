using OrderService.Core.ProductAggregate;
using OrderService.SharedKernel;

namespace OrderService.Core.OrderAggregate.Events;
public class OrderDetailUpdatedWeightEvent : DomainEventBase
{
  public Order order { get; set; }
  public Product product { get; set; }

  public OrderDetailUpdatedWeightEvent(Order order, Product product)
  {
    this.order = order;
    this.product = product;
  }
}
