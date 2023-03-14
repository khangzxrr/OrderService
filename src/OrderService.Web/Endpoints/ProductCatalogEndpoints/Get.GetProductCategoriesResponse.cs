namespace OrderService.Web.Endpoints.ProductCatalogEndpoints;

public class GetProductCategoriesResponse
{
  public IEnumerable<ProductCategoryRecord> Categories { get; set; }
  public GetProductCategoriesResponse(IEnumerable<ProductCategoryRecord> categories)
  {
    Categories = categories;
  }

}
