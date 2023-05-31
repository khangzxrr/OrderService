using OrderService.Web.Endpoints.BaseEndpoints;
using OrderService.Web.Endpoints.Records;

namespace OrderService.Web.Endpoints.EmployeeEndpoints;

public class GetProductIssuesResponse : BasePaginatedResponse<ProductIssueRecord>
{
  public GetProductIssuesResponse(int totalCount, int pageSize, IEnumerable<ProductIssueRecord> records) : base(totalCount, pageSize, records)
  {
  }
}
