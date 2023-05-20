using OrderService.Web.Endpoints.Records;

namespace OrderService.Web.Endpoints.OrderEndpoints;

public class GetByIdResponse
{
  public OrderRecord order { get; set; }

  public GetByIdResponse(OrderRecord order)
  {
    this.order = order;
  }
}
