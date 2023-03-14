namespace OrderService.Web.Endpoints.ProductCatalogEndpoints;

public class CreateProductCatalogResponse
{
  public ProductCategoryRecord Category { get; set; }

  public CreateProductCatalogResponse(ProductCategoryRecord category)
  {
    this.Category = category;
  }
}
