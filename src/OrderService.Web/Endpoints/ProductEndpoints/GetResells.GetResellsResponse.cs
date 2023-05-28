using OrderService.Web.Endpoints.BaseEndpoints;
using OrderService.Web.Endpoints.Records;

namespace OrderService.Web.Endpoints.ProductEndpoints;

public class GetResellsResponse : BasePaginatedResponse<ProductRecord>
{

  public GetResellsResponse(int totalCount, int pageSize, IEnumerable<ProductRecord> records) : base(totalCount, pageSize, records)
  {
  }
}
