namespace OrderService.Web.Endpoints.ManagerEndpoints;

public class GetTotalOrderShippingsResponse
{
  public int totalOrderShippings { get; set; }

  public GetTotalOrderShippingsResponse(int totalOrderShippings)
  {
    this.totalOrderShippings = totalOrderShippings;
  }
}
