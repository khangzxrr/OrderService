using OrderService.Web.Endpoints.Records;

namespace OrderService.Web.Endpoints.EmployeeEndpoints;

public class CreateOrderShippingResponse
{
  public OrderShippingRecord? orderShippingRecord { get; set; }
  public string message { get; set; }

  public CreateOrderShippingResponse(OrderShippingRecord? orderShippingRecord, string message = "")
  {
    this.orderShippingRecord = orderShippingRecord;
    this.message = message; 
  }

}
