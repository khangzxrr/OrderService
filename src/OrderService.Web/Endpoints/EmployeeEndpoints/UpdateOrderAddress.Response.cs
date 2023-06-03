namespace OrderService.Web.Endpoints.EmployeeEndpoints;

public class UpdateOrderAddressResponse
{
  public string message { get; set; }
  
  public UpdateOrderAddressResponse(string message)
  {
    this.message = message;
  }
}
