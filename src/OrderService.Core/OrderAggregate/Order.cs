
using Ardalis.GuardClauses;
using OrderService.Core.ChatAggregate;
using OrderService.SharedKernel;
using OrderService.SharedKernel.Interfaces;

namespace OrderService.Core.OrderAggregate;
public class Order : EntityBase, IAggregateRoot
{
  public Chat chat { get; private set; }
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

  private readonly List<OrderDetail> _orderDetails = new List<OrderDetail>();
  public IReadOnlyCollection<OrderDetail> orderDetails => _orderDetails.AsReadOnly();

  public void addOrderDetail(OrderDetail orderDetail)
  {
    Guard.Against.Null(orderDetail);
    _orderDetails.Add(orderDetail);
  }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
  public Order(
    string orderDescription,
    string customerDescription,
    string deliveryAddress,
    string contactPhonenumber)
  {
    orderDate = DateTime.Now;
    status = OrderStatus.noPayYet;
    this.orderDescription = Guard.Against.NullOrEmpty(orderDescription, nameof(orderDescription));
    this.customerDescription = Guard.Against.NullOrEmpty(customerDescription, nameof(customerDescription));
    this.deliveryAddress = Guard.Against.NullOrEmpty(deliveryAddress, nameof(deliveryAddress));
    this.contactPhonenumber = Guard.Against.NullOrEmpty(contactPhonenumber, nameof(contactPhonenumber));

    shippingEstimatedDays = 30;
    price = 0;
  }

  public void SetChat(Chat chat)
  {
    this.chat = Guard.Against.Null(chat);
  }

}
