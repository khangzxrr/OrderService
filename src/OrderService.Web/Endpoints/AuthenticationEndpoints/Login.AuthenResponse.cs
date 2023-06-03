namespace OrderService.Web.Endpoints.AuthenticationEndpoints;

public class AuthenResponse
{
  public string token { get; set; }
  public string email { get; set; }
  public string address { get; set; }
  public string phoneNumber { get; set; }

  public string roleName { get; set; }

  public string fullName { get; set; }

  public AuthenResponse(string token, string email, string address, string phoneNumber, string roleName, string fullName)
  {
    this.token = token;
    this.email = email;
    this.address = address;
    this.phoneNumber = phoneNumber;
    this.roleName = roleName;
    this.fullName = fullName;
  }
}
