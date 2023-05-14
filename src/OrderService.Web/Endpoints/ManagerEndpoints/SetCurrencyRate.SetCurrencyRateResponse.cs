namespace OrderService.Web.Endpoints.ManagerEndpoints;

public class SetCurrencyRateResponse
{
  public string message { get; set; }

  public SetCurrencyRateResponse(string message)
  {
    this.message = message;
  }
}
