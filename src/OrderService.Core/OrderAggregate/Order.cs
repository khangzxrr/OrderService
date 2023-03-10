
using Ardalis.GuardClauses;
using OrderService.SharedKernel;
using OrderService.SharedKernel.Interfaces;

namespace OrderService.Core.OrderAggregate;
public class Order : EntityBase, IAggregateRoot
{
  public DateTime orderDate { get; }
  public OrderStatus status { get; }
  public string orderDescription { get; }
  public string customerDescription { get; }
  public string deliveryAddress { get; }
  public string contactPhonenumber { get; }
  public int shippingEstimatedDays { get; }
  public float price;

  private readonly List<OrderPayment> _orderPayment = new List<OrderPayment>();
  public IReadOnlyCollection<OrderPayment> orderPayments => _orderPayment.AsReadOnly();



  public Order(
    DateTime orderDate, 
    OrderStatus status,
    string orderDescription,
    string customerDescription,
    string deliveryAddress,
    string contactPhonenumber,
    int shippingEstimatedDays,
    float price)
  {
    this.orderDate = Guard.Against.Null(orderDate, nameof(orderDate));
    this.status = Guard.Against.Null(status, nameof(status));
    this.orderDescription = Guard.Against.NullOrEmpty(orderDescription, nameof(orderDescription));
    this.customerDescription = Guard.Against.NullOrEmpty(customerDescription, nameof(customerDescription));
    this.deliveryAddress = Guard.Against.NullOrEmpty(deliveryAddress, nameof(deliveryAddress));
    this.contactPhonenumber = Guard.Against.NullOrEmpty(contactPhonenumber, nameof(contactPhonenumber));
    this.shippingEstimatedDays = Guard.Against.Negative(shippingEstimatedDays, nameof(shippingEstimatedDays));
    this.price = Guard.Against.Negative(price, nameof(price));
  }

}
