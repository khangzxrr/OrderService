using OrderService.Core.CurrencyAggregate;

namespace OrderService.Web.Endpoints.ManagerEndpoints;

public record CurrencyRecord(int id, string fromCurrency, float rate)
{
  public static CurrencyRecord fromEntity(CurrencyExchange currencyExchange)
  {
    return new CurrencyRecord(currencyExchange.Id, currencyExchange.fromCurrency, currencyExchange.rate);
  }
}
