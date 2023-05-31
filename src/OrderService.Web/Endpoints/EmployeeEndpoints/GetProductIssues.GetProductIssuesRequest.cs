using OrderService.Web.Endpoints.BaseEndpoints;

namespace OrderService.Web.Endpoints.EmployeeEndpoints;

public class GetProductIssuesRequest : BasePaginatedRequest
{
  public const string Route = "/employee/productIssues";


}
