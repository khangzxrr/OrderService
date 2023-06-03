namespace OrderService.Web.Endpoints.AuthenticationEndpoints;

public class UpdateProfileResponse
{
  public string fullName { get; set; }
  public string address { get; set; }
  public string phoneNumber { get; set; }


  public UpdateProfileResponse(string fullName, string address, string phoneNumber)
  {
    this.fullName = fullName;
    this.address = address;
    this.phoneNumber = phoneNumber;
  }
}
