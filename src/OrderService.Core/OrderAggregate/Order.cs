using Ardalis.GuardClauses;
using OrderService.SharedKernel;
using OrderService.SharedKernel.Interfaces;

namespace OrderService.Core.OrderAggregate;
public class Order : EntityBase, IAggregateRoot
{
  public Chat chat { get; private set; }
  public DateTime orderDate { get; }
  public OrderStatus status { get; private set; }
  public string orderDescription { get; }
  public string customerDescription { get; }
  public string deliveryAddress { get; }
  public string contactPhonenumber { get; }
  public int shippingEstimatedDays { get; }
  public float price;

  private readonly List<OrderPayment> _orderPayments = new();
  public IEnumerable<OrderPayment> orderPayments => _orderPayments.AsReadOnly();

  private readonly List<OrderDetail> _orderDetails = new();
  public IEnumerable<OrderDetail> orderDetails => _orderDetails.AsReadOnly();

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

  public void SetStatus(OrderStatus status)
  {
    this.status = Guard.Against.Null(status);
  }

  public float GetFirstPaymentAmount()
  {
    return 80.0f * this.price / 100.0f;
  }

  public float GetSecondPaymentAmount()
  {
    return 20.0f * this.price / 100.0f;
  }

  public void AddPayment(OrderPayment payment)
  {
    Guard.Against.Null(payment);

    _orderPayments.Add(payment);
  }
}
