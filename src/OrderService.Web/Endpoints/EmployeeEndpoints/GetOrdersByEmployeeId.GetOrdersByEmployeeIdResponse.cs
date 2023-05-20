using OrderService.Web.Endpoints.Records;

namespace OrderService.Web.Endpoints.EmployeeEndpoints;

public class GetOrdersByEmployeeIdResponse
{
  public IEnumerable<OrderRecord> orderRecords { get; set; }

  public GetOrdersByEmployeeIdResponse(IEnumerable<OrderRecord> orderRecords)
  {
    this.orderRecords = orderRecords;
  } 
}
