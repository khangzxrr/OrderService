using Ardalis.GuardClauses;
using OrderService.SharedKernel;
using OrderService.SharedKernel.Interfaces;

namespace OrderService.Core.OrderAggregate;
public class ReturnPayment : EntityBase
{
  public float cost { get; set; }
  public DateTime paymentDate { get; set; }
  public string paymentDescription { get; set; }

  public ReturnPayment(float cost, DateTime paymentDate, string paymentDescription)
  {
    this.cost = Guard.Against.Negative(cost);
    this.paymentDate = Guard.Against.Null(paymentDate);
    this.paymentDescription = Guard.Against.NullOrEmpty(paymentDescription);
  }
}
