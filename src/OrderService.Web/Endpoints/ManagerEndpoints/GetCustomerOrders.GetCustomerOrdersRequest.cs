using System.ComponentModel.DataAnnotations;
using OrderService.Web.Endpoints.BaseEndpoints;

namespace OrderService.Web.Endpoints.ManagerEndpoints;

public class GetCustomerOrdersRequest : BasePaginatedRequest
{
  public const string Route = "/managers/customer/orders";

  [Required]
  public int customerId { get; set; }
}
