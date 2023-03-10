using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using OrderService.SharedKernel;

namespace OrderService.Core.OrderAggregate;
public class OrderPayment : EntityBase
{
  public PaymentStatus paymentStatus { get; }
  public float paymentCost { get; }
  public DateTime paymentDate { get; }
  public string paymentDescription { get; }

  public OrderPayment(PaymentStatus paymentStatus, float paymentCost, DateTime paymentDate)
  {
    this.paymentStatus = Guard.Against.Null(paymentStatus, nameof(paymentStatus));
    this.paymentCost = Guard.Against.Negative(paymentCost, nameof(paymentCost));
    this.paymentDate = Guard.Against.Null(paymentDate, nameof(paymentDate));
    this.paymentDescription = Guard.Against.NullOrEmpty(paymentDescription, nameof(paymentDescription));
  }

 
}
