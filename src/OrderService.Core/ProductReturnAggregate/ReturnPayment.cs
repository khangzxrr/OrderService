using Ardalis.GuardClauses;
using OrderService.SharedKernel;

namespace OrderService.Core.ProductReturnAggregate;
public class ReturnPayment : EntityBase
{
  public float cost { get; set; }
  public DateTime paymentDate { get; set; }
  public string paymentDescription { get; set; }

  public ReturnPaymentStatus paymentStatus { get; private set; }

  public bool isPaid { get; private set; }

  public ReturnPayment(float cost, string paymentDescription)
  {
    this.cost = Guard.Against.Negative(cost);
    
    this.paymentDescription = Guard.Against.NullOrEmpty(paymentDescription);

    paymentStatus = ReturnPaymentStatus.customerPay;

    paymentDate = DateTime.Now;
    isPaid = false;
  }


}
