using Microsoft.Build.Framework;

namespace OrderService.Web.Endpoints.ProductEndpoints;

public class PeekProductRequest
{
  public const string Route = "/products/request";

  [Required]
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
  public string ProductUrl { get; set;}
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

}
