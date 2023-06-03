using System.ComponentModel.DataAnnotations;

namespace OrderService.Web.Endpoints.AuthenticationEndpoints;

public class UpdateProfileRequest
{
  public const string Route = "/updateProfile";

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

  [Required]
  public string fullName { get; set; }
  [Required]
  public string password { get; set; }
  [Required]
  public string phoneNumber { get; set; }
  [Required]
  public string address { get; set; }
}
