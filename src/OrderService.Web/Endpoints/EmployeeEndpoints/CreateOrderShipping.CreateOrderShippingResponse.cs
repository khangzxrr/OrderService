namespace OrderService.Web.Endpoints.EmployeeEndpoints;

public class CreateOrderShippingResponse
{
  public OrderShippingRecord orderShippingRecord { get; set; }

  public CreateOrderShippingResponse(OrderShippingRecord orderShippingRecord)
  {
    this.orderShippingRecord = orderShippingRecord;
  }

}
