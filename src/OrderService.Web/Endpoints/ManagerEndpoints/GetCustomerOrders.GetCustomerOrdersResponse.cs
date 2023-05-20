using OrderService.Web.Endpoints.BaseEndpoints;
using OrderService.Web.Endpoints.Records;

namespace OrderService.Web.Endpoints.ManagerEndpoints;

public class GetCustomerOrdersResponse : BasePaginatedResponse<GeneralOrderRecord>
{
  public GetCustomerOrdersResponse(int totalCount, int pageSize, IEnumerable<GeneralOrderRecord> records) : base(totalCount, pageSize, records)
  {
  }
}
