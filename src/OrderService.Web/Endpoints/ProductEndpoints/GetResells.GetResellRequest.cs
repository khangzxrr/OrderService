using OrderService.Web.Endpoints.BaseEndpoints;

namespace OrderService.Web.Endpoints.ProductEndpoints;

public class GetResellsRequest : BasePaginatedRequest
{
  public const string Route = "/products/resells";


}
