using Ardalis.GuardClauses;
using OrderService.SharedKernel;
using OrderService.SharedKernel.Interfaces;

namespace OrderService.Core.CurrencyAggregate;
public class CurrencyExchange : EntityBase, IAggregateRoot
{
  public string fromCurrency { get; set; }
  public float rate { get; set; }

  public CurrencyExchange(string fromCurrency, float rate)
  {
    this.fromCurrency = Guard.Against.NullOrEmpty(fromCurrency);
    this.rate = Guard.Against.Negative(rate);
  }
}
