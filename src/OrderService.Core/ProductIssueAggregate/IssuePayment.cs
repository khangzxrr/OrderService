using Ardalis.GuardClauses;
using OrderService.SharedKernel;

namespace OrderService.Core.ProductReturnAggregate;
public class IssuePayment : EntityBase
{
  public float cost { get; set; }
  public DateTime paymentDate { get; set; }
  public string paymentDescription { get; set; }

  public IssuePaymentStatus paymentStatus { get; private set; }

  public bool isPaid { get; private set; }

  public IssuePayment(float cost, string paymentDescription)
  {
    this.cost = Guard.Against.Negative(cost);
    
    this.paymentDescription = Guard.Against.NullOrEmpty(paymentDescription);

    paymentStatus = IssuePaymentStatus.customerPay;

    paymentDate = DateTime.Now;
    isPaid = false;
  }


}
