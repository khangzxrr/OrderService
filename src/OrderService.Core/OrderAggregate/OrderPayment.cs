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
  public double paymentCost { get; }
  public DateTime paymentDate { get; }
  public string paymentDescription { get; }
  public string transactionalId { get; private set; }

  public OrderPayment(PaymentStatus paymentStatus, double paymentCost, string transactionalId, string paymentDescription, DateTime paymentDate)
  {
    this.paymentStatus = Guard.Against.Null(paymentStatus, nameof(paymentStatus));
    this.paymentCost = Guard.Against.Negative(paymentCost, nameof(paymentCost));
    this.paymentDate = Guard.Against.Null(paymentDate, nameof(paymentDate));
    this.transactionalId = Guard.Against.NullOrEmpty(transactionalId);
    this.paymentDescription = Guard.Against.NullOrEmpty(paymentDescription, nameof(paymentDescription));
  }

  public void SetTransactionalId(string transactionalId)
  {
    transactionalId = Guard.Against.NullOrEmpty(transactionalId);
  }

  public static long ConvertDollarToVnPayVND(double cost)
  {
    return (long)(cost * 100000);
  }

 
}
