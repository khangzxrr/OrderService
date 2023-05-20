using OrderService.Web.Endpoints.Records;

namespace OrderService.Web.Endpoints.OrderEndpoints;

public class CreateOrderResponse
{
  public OrderRecord order { get; set; }
  public CreateOrderResponse(OrderRecord order)
  {
    this.order = order;
  }
}
