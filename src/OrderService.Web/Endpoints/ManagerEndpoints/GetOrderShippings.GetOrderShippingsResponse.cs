using OrderService.Web.Endpoints.Records;

namespace OrderService.Web.Endpoints.ManagerEndpoints;

public class GetOrderShippingsResponse
{

  public int pageCount { get; set; }

  public IEnumerable<OrderShippingRecord> orderShippingRecords { get; set; }

  public GetOrderShippingsResponse(int pageCount, IEnumerable<OrderShippingRecord> orderShippingRecords)
  {
    this.pageCount = pageCount;
    this.orderShippingRecords = orderShippingRecords;
  }
}
