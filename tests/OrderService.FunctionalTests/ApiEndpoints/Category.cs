
using Ardalis.HttpClientTestExtensions;
using Newtonsoft.Json;
using OrderService.Web;
using OrderService.Web.Endpoints.ProductCatalogEndpoints;
using Xunit;

namespace OrderService.FunctionalTests.ApiEndpoints;

[Collection("Sequential")]
public class Category : IClassFixture<CustomWebApplicationFactory<WebMarker>>
{

  private readonly HttpClient _httpClient;

  public Category(CustomWebApplicationFactory<WebMarker> customWebApplicationFactory)
  {
    _httpClient = customWebApplicationFactory.CreateClient();
  }

  [Fact]
  public async Task EnsureSuccessCreateNewCategory()
  {
    var request = new CreateProductCatalogRequest("new category");

    var json = new StringContent(JsonConvert.SerializeObject(request), System.Text.Encoding.UTF8, "application/json");

    var result = await _httpClient.PostAndDeserializeAsync<CreateProductCatalogResponse>("/categories", json);

    Assert.Equal(request.CategoryName, result.Category.categoryName);
  }

  

}
