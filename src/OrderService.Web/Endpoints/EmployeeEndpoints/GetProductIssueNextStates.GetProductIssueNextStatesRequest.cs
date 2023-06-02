using System.ComponentModel.DataAnnotations;

namespace OrderService.Web.Endpoints.EmployeeEndpoints;

public class GetProductIssueNextStatesRequest
{
  public const string Route = "/employees/productIssues/nextstates";

  [Required]
  public int productIssueId { get; set; }

}
