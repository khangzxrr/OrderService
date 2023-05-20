using System.ComponentModel.DataAnnotations;
using OrderService.Web.Endpoints.BaseEndpoints;

namespace OrderService.Web.Endpoints.ManagerEndpoints;

public class GetShipperOrdersRequest: BasePaginatedRequest
{
  public const string Route = "/manager/shippers/orders";

  [Required]
  public int shipperId { get; set; }
}
