namespace OrderService.Web.Endpoints.OrderEndpoints;

public class SendMessageResponse
{
  public string message { get; set; }

  public SendMessageResponse(string message)
  {
    this.message = message;
  } 
}
