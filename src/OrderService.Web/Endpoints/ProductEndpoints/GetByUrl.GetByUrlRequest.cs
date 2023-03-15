using System.ComponentModel.DataAnnotations;

namespace OrderService.Web.Endpoints.ProductEndpoints;

public class GetByUrlRequest
{

  public const string Route = "/products";

  [Required]
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
  public string Url { get; set; }

}
