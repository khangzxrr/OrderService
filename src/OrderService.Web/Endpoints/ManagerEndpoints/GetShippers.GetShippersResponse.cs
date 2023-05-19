using OrderService.Core.ShipperAggregate;
using OrderService.Web.Endpoints.BaseEndpoints;
using OrderService.Web.Endpoints.Records;

namespace OrderService.Web.Endpoints.ManagerEndpoints;

public class GetShippersResponse : BasePaginatedResponse<ShipperRecord>
{
  public GetShippersResponse(int totalCount, int pageSize, IEnumerable<ShipperRecord> records) : base(totalCount, pageSize, records)
  {
  }
}
