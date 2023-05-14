namespace OrderService.Web.Endpoints.ManagerEndpoints;

public class GetCurrenciesResponse
{
  public IEnumerable<CurrencyRecord> currencyRecords { get; set; }

  public GetCurrenciesResponse(IEnumerable<CurrencyRecord> currencyRecords)
  {
    this.currencyRecords = currencyRecords;
  }
}
