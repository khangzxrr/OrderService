using OrderService.Web.Endpoints.BaseEndpoints;
using OrderService.Web.Endpoints.Records;

namespace OrderService.Web.Endpoints.ManagerEndpoints;

public class GetCustomersResponse : BasePaginatedResponse<CustomerRecord>
{
  public GetCustomersResponse(int totalCount, int pageSize, IEnumerable<CustomerRecord> records) : base(totalCount, pageSize, records)
  {
  }
}
