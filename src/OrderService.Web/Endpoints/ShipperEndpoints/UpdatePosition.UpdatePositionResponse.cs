namespace OrderService.Web.Endpoints.ShipperEndpoints;

public class UpdatePositionResponse
{
  public string message { get; set; }

  public UpdatePositionResponse(string message)
  {
    this.message = message;
  }
}
