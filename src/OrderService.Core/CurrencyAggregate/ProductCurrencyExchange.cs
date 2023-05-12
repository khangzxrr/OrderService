using Ardalis.GuardClauses;
using OrderService.Core.ProductAggregate;
using OrderService.SharedKernel;

namespace OrderService.Core.CurrencyAggregate;
public class ProductCurrencyExchange : EntityBase
{
  public int currencyId { get; set; }
  public int productId { get; set; }

  public float rate { get; set; }
  public DateTime productCurrencyCreateAt { get; set; }

  public Product product { get; set; }
  public CurrencyExchange currency { get; set; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
  public ProductCurrencyExchange(float rate)
  {
    setRate(rate);
    this.productCurrencyCreateAt = DateTime.Now;
  }

  public void setProduct(Product product) { 
    this.product = Guard.Against.Null(product); 
  }

  public void setRate(float rate) {  
    this.rate = Guard.Against.NegativeOrZero(rate); 
  }

  public void setCurrency(CurrencyExchange currency) {
    this.currency = Guard.Against.Null(currency);
  }
}
