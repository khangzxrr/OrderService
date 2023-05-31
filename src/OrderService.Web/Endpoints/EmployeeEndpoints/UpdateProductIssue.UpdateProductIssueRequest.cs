using System.ComponentModel.DataAnnotations;

namespace OrderService.Web.Endpoints.EmployeeEndpoints;

public class UpdateProductIssueRequest
{
  public const string Route = "/employee/productIssues";


  public string? status { get; set; } = null;
  public string? finishStatus { get; set; } = null;

  [Required]
  public int productIssueId { get; set; } 


}
