using Ardalis.GuardClauses;
using OrderService.Core.OrderPaymentAggregate;
using OrderService.Core.UserAggregate;
using OrderService.SharedKernel;
using OrderService.SharedKernel.Interfaces;

namespace OrderService.Core.OrderAggregate;
public class Order : EntityBase, IAggregateRoot
{
  public int userId { get; }
  public User user { get; }
  public Chat chat { get; private set; }
  public DateTime orderDate { get; }
  public OrderStatus status { get; private set; }
  public string orderDescription { get; }
  public string customerDescription { get; }
  public string deliveryAddress { get; }
  public string contactPhonenumber { get; }
  public int shippingEstimatedDays { get; }

  public double price { get; private set; }
  public double remainCost { get; private set; }

  public OrderLocalShippingStatus localShippingStatus { get; private set; }

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
    
    this.orderDescription = "";
    this.customerDescription = Guard.Against.Null(customerDescription);
    this.deliveryAddress = Guard.Against.NullOrEmpty(deliveryAddress, nameof(deliveryAddress));
    this.contactPhonenumber = Guard.Against.NullOrEmpty(contactPhonenumber, nameof(contactPhonenumber));

    orderDate = DateTime.Now;
    status = OrderStatus.noPriceQuotation;

    shippingEstimatedDays = 30;
    price = 0;
    remainCost = price;

    localShippingStatus = OrderLocalShippingStatus.notInQueue;
  }

  public void SetQueueInShipping(OrderLocalShippingStatus status) {
    localShippingStatus = Guard.Against.Null(status);
  }

  public void SetChat(Chat chat)
  {
    this.chat = Guard.Against.Null(chat);
  }

  public void SetPrice(double price) { 
    this.price = Guard.Against.Negative(price);

    this.remainCost = price;
  }
  public void SetRemainCost(double remainCost)
  {
    this.remainCost = Guard.Against.Negative(remainCost);
  }

  public void SetStatus(OrderStatus status)
  {
    this.status = Guard.Against.Null(status);
  }

  public double GetFirstPaymentAmount()
  {
    return Math.Ceiling(80.0f * this.price / 100.0f);
  }

  public double GetSecondPaymentAmount()
  {
    return remainCost;
  }

  public bool IsPaidFirstMilestone()
  {
    return orderPayments.Where(op => op.paymentStatus == PaymentStatus.firstPayment).Any();
  }

  public bool IsPaidAllMilestone()
  {
    return orderPayments.Where(op => op.paymentStatus == PaymentStatus.firstPayment || op.paymentStatus == PaymentStatus.SecondPayment).Count() >= 2;
  }

  public double GetTotalPaymentsAmount()
  {
    return orderPayments.Sum(op => op.paymentCost);
  }

  public void AddPayment(OrderPayment payment)
  {
    Guard.Against.Null(payment);

    _orderPayments.Add(payment);
  }
}
