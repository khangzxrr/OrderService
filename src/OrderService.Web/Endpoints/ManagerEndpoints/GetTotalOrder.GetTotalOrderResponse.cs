namespace OrderService.Web.Endpoints.ManagerEndpoints;

public class GetTotalOrderResponse
{
  public int totalOrder { get; set; }

  public GetTotalOrderResponse(int totalOrder)
  {
    this.totalOrder = totalOrder;
  }
}
