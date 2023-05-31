using System.ComponentModel.DataAnnotations;

namespace OrderService.Web.Endpoints.ProductIssueEndpoints;

public class IsExistActiveProductIssueRequest
{
  public const string Route = "/productIssue/isExistActive";

  [Required]
  public int productId { get; set; }
}
