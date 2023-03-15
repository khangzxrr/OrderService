namespace OrderService.Web.Endpoints.AuthenticationEndpoints;

public class RegisterResponse
{
  public string Token { get; set; }

  public RegisterResponse(string token)
  {
    Token = token;
  }
}
