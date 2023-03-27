
using Ardalis.HttpClientTestExtensions;
using Newtonsoft.Json;
using OrderService.Web;
using OrderService.Web.Endpoints.AuthenticationEndpoints;
using Xunit;

namespace OrderService.FunctionalTests.ApiEndpoints;

[Collection("Sequential")]
public class Login  : IClassFixture<CustomWebApplicationFactory<WebMarker>>
{
  private readonly HttpClient _httpClient;

  public Login(CustomWebApplicationFactory<WebMarker> customWebApplicationFactory)
  {
    _httpClient = customWebApplicationFactory.CreateClient();
  }

  [Fact]
  public async Task ReturnTokenAndBasicInformation()
  {
    var authenRequest = new AuthenRequest("customer@gmail.com", "123123");

    var jsonObject = JsonConvert.SerializeObject(authenRequest);

    var jsonContent = new StringContent(jsonObject, System.Text.Encoding.UTF8, "application/json");

    Console.WriteLine(jsonContent);

    var result = await _httpClient.PostAndDeserializeAsync<AuthenResponse>("/login", jsonContent);

    Assert.Equal(SeedData.customer.email, result.email);
    Assert.Equal(SeedData.customer.address, result.address);
  }

  [Fact]
  public async Task ReturnBadRequestWhenWrongEmailOrPassword()
  {
    var authenRequest = new AuthenRequest("abc", "xyz");

    var jsonContent = new StringContent(JsonConvert.SerializeObject(authenRequest), System.Text.Encoding.UTF8, "application/json");

    await _httpClient.PostAndEnsureBadRequestAsync("/login", jsonContent);
  }

}
