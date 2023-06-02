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

  public IssuePayment(float cost, IssuePaymentStatus paymentStatus, string paymentDescription, bool isPaid = false)
  {
    this.cost = Guard.Against.Negative(cost);
    
    this.paymentDescription = Guard.Against.NullOrEmpty(paymentDescription);

    this.paymentStatus = Guard.Against.Null(paymentStatus);

    paymentDate = DateTime.Now;
    this.isPaid = Guard.Against.Null(isPaid);
  }

  public void setIsPaid(bool isPaid)
  {
    this.isPaid = Guard.Against.Null(isPaid);
  }


}
