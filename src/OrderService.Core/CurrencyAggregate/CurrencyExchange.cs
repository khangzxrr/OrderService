using Ardalis.GuardClauses;
using OrderService.SharedKernel;
using OrderService.SharedKernel.Interfaces;

namespace OrderService.Core.CurrencyAggregate;
public class CurrencyExchange : EntityBase, IAggregateRoot
{
  public string fromCurrency { get; private set; }
  public float rate { get; private set; }

  public CurrencyExchange(string fromCurrency, float rate)
  {
    this.fromCurrency = Guard.Against.NullOrEmpty(fromCurrency);
    this.rate = Guard.Against.Negative(rate);
  }

  public void setRate(float rate)
  {
    this.rate = Guard.Against.NegativeOrZero(rate);
  }
}
