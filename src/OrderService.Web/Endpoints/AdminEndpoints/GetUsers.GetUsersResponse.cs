using OrderService.Web.Endpoints.BaseEndpoints;
using OrderService.Web.Endpoints.Records;

namespace OrderService.Web.Endpoints.AdminEndpoints;

public class GetUsersResponse : BasePaginatedResponse<UserRecord>
{
  public GetUsersResponse(int totalCount, int pageSize, IEnumerable<UserRecord> records) : base(totalCount, pageSize, records)
  {
  }
}
