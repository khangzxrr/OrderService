namespace OrderService.Web.Endpoints.ShipperEndpoints;

public class PayRemainCostResponse
{
  public string message { get; set; }

  public PayRemainCostResponse(string message)
  {
    this.message = message;
  }
}
