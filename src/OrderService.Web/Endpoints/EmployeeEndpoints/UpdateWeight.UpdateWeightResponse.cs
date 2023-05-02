namespace OrderService.Web.Endpoints.EmployeeEndpoints;

public class UpdateWeightResponse
{
  public double updatedWeight { get; set; }

  public UpdateWeightResponse(double updatedWeight)
  {
    this.updatedWeight = updatedWeight;
  }
}
