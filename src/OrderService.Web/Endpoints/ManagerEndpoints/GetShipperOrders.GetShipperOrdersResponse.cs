using OrderService.Web.Endpoints.BaseEndpoints;
using OrderService.Web.Endpoints.OrderEndpoints;
using OrderService.Web.Endpoints.Records;

namespace OrderService.Web.Endpoints.ManagerEndpoints;

public class GetShipperOrdersResponse : BasePaginatedResponse<OrderShippingRecord>
{
  public GetShipperOrdersResponse(int totalCount, int pageSize, IEnumerable<OrderShippingRecord> records) : base(totalCount, pageSize, records)
  {
  }
}
