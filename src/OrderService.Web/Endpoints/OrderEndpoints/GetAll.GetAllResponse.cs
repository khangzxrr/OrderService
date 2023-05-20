using OrderService.Web.Endpoints.Records;

namespace OrderService.Web.Endpoints.OrderEndpoints;

public class GetAllResponse
{
  public IEnumerable<GeneralOrderRecord> orderRecords { get; set; }

  public GetAllResponse(IEnumerable<GeneralOrderRecord> orderRecords)
  {
    this.orderRecords = orderRecords;
  }
}
