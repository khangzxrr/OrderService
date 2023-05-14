namespace OrderService.Web.Endpoints.EmployeeEndpoints;

public class MarkUserTakenOrderShippingResponse
{
  public string message { get; set; }

  public MarkUserTakenOrderShippingResponse(string message)
  {
    this.message = message;
  }
}
