using Microsoft.Build.Framework;

namespace OrderService.Web.Endpoints.ProductCatalogEndpoints;

public class CreateProductCatalogRequest
{
  public const string Route = "/categories";

  [Required]
  public string? CategoryName { get; set; }                   

}
