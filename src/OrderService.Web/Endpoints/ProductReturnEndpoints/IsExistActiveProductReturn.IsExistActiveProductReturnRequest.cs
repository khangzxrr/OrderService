using System.ComponentModel.DataAnnotations;

namespace OrderService.Web.Endpoints.ProductReturnEndpoints;

public class IsExistActiveProductReturnRequest
{
  public const string Route = "/productReturn/isExistActive";

  [Required]
  public int productId { get; set; }
}
