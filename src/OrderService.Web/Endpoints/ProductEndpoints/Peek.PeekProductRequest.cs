using Microsoft.Build.Framework;

namespace OrderService.Web.Endpoints.ProductEndpoints;

public class PeekProductRequest
{
  public const string Route = "/products/request";

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

  [Required]
  public string ProductUrl { get; set;}
  [Required]
  public string ConnectionId { get; set;}



}
