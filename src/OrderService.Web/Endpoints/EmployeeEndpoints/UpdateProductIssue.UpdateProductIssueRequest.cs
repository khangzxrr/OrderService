using System.ComponentModel.DataAnnotations;

namespace OrderService.Web.Endpoints.EmployeeEndpoints;

public class UpdateProductIssueRequest
{
  public const string Route = "/employee/productIssues";

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

  [Required]
  public string status { get; set; }

  [Required]
  public int productIssueId { get; set; } 


}
