namespace OrderService.Web.Endpoints.OrderEndpoints;

public class PayResponse
{
  public string PayUrl { get; set; }

  public PayResponse(string payUrl)
  {
    PayUrl = payUrl;
  }
}
