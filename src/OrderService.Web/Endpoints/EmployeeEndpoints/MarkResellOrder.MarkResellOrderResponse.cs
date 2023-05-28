using OrderService.Web.Endpoints.Records;

namespace OrderService.Web.Endpoints.EmployeeEndpoints;

public class MarkResellOrderResponse 
{
  public OrderRecord order { get; set; }

  public MarkResellOrderResponse(OrderRecord orderRecord)
  {
    this.order = orderRecord;
  }
}
