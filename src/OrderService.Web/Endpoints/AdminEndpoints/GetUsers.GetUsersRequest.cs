using OrderService.Web.Endpoints.BaseEndpoints;

namespace OrderService.Web.Endpoints.AdminEndpoints;

public class GetUsersRequest: BasePaginatedRequest
{
  public const string Route = "/admin/users";
}
