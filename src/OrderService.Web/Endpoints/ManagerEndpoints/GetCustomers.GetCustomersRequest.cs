using OrderService.Web.Endpoints.BaseEndpoints;

namespace OrderService.Web.Endpoints.ManagerEndpoints;

public class GetCustomersRequest : BasePaginatedRequest
{
  public const string Route = "/manager/customers";
}
