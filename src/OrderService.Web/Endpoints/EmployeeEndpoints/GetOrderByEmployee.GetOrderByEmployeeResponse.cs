using OrderService.Web.Endpoints.Records;

namespace OrderService.Web.Endpoints.EmployeeEndpoints;

public class GetOrderByEmployeeResponse
{
  public OrderRecord order { get; set; }

  public GetOrderByEmployeeResponse(OrderRecord order)
  {
    this.order = order;
  }
}
