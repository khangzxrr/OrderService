using OrderService.Web.Endpoints.BaseEndpoints;

namespace OrderService.Web.Endpoints.ManagerEndpoints;

public class GetShippersRequest : BasePaginatedRequest
{
  public const string Route = "manager/shippers";

}
