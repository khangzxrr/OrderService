using OrderService.Web.Endpoints.OrderEndpoints;

namespace OrderService.Web.Endpoints.EmployeeEndpoints;

public class GetOrdersByEmployeeIdResponse
{
  public IEnumerable<OrderRecord> orderRecords { get; set; }

  public GetOrdersByEmployeeIdResponse(IEnumerable<OrderRecord> orderRecords)
  {
    this.orderRecords = orderRecords;
  } 
}
