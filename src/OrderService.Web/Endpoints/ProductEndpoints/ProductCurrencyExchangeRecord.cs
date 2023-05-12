using OrderService.Core.CurrencyAggregate;

namespace OrderService.Web.Endpoints.ProductEndpoints;

public record ProductCurrencyExchangeRecord(string currencyName, float rate)
{
  public static ProductCurrencyExchangeRecord FromEntity(ProductCurrencyExchange productCurrencyExchange)
  {
    return new ProductCurrencyExchangeRecord(productCurrencyExchange.currency.fromCurrency, productCurrencyExchange.rate);
  }
}
