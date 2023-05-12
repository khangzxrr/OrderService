namespace OrderService.Web.Endpoints.AuthenticationEndpoints;

public class RegisterResponse
{
  public string token { get; set; }
  public string email { get; set; }
  public string address { get; set; }
  public string phoneNumber { get; set; }

  public string roleName { get; set; }
  public RegisterResponse(string token, string email, string address, string phoneNumber, string roleName)
  {
    this.token = token;
    this.email = email;
    this.address = address;
    this.phoneNumber = phoneNumber;
    this.roleName = roleName;
  }
}
