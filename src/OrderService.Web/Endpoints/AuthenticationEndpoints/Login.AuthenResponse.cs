namespace OrderService.Web.Endpoints.AuthenticationEndpoints;

public class AuthenResponse
{
  public string token { get; set; }

  public AuthenResponse(string token)
  {
    this.token = token;
  }
}
