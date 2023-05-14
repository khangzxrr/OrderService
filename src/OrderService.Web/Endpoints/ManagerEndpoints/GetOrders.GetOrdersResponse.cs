using OrderService.Web.Endpoints.OrderEndpoints;

namespace OrderService.Web.Endpoints.ManagerEndpoints;

public class GetOrdersResponse
{
  public int pageCount { get; set; }

  public IEnumerable<GeneralOrderRecord> orderRecords { get; set; }

  public GetOrdersResponse(int pageCount, IEnumerable<GeneralOrderRecord> orderRecords)
  {
    this.pageCount = pageCount;
    this.orderRecords = orderRecords;
  }
}
