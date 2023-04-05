namespace OrderService.Web.Endpoints.EmployeeEndpoints;

public class UpdateOrderStatusResponse
{
  public string message { get; set; }

  public UpdateOrderStatusResponse(string message)
  {
    this.message = message;
  }
}
