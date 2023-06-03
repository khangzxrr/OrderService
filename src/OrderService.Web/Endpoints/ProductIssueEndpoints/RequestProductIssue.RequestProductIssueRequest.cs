
using System.ComponentModel.DataAnnotations;

namespace OrderService.Web.Endpoints.ProductIssueEndpoints;

public class RequestProductIssueRequest
{
  public const string Route = "/productIssue/request";

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.


  [Required]
  public string[] medias { get; set; }

  [Required]
  public bool isWarranty { get; set; }

  [Required]
  public int orderDetailId { get; set; }


  [Required]
  public string? series { get; set; }

  [Required]
  public string? description { get; set; }



}
