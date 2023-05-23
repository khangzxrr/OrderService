using System.ComponentModel.DataAnnotations;

namespace OrderService.Web.Endpoints.ProductCatalogEndpoints;

public class CreateProductCatalogRequest
{
  public const string Route = "/categories";

  [Required]
  public string CategoryName { get; set; }                  
  
  public CreateProductCatalogRequest(string categoryName)
  {
    CategoryName = categoryName;
  }

}
