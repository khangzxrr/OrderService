using OrderService.Web.Endpoints.Records;

namespace OrderService.Web.Endpoints.EmployeeEndpoints;

public class UpdateOrderDetailResponse
{
  public OrderRecord order { get; set; }

  public UpdateOrderDetailResponse(OrderRecord orderRecord)
  {
    this.order = orderRecord;
  }
}
