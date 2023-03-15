using System.ComponentModel.DataAnnotations;

namespace OrderService.Web.Endpoints.AuthenticationEndpoints;

public class AuthenRequest
{
  [Required]
  public string Email { get; set; }
  [Required] 
  public string Password { get; set; }

  public AuthenRequest(string email, string password)
  {
    Email = email;
    Password = password;
  }
}
