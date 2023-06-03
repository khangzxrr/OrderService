using System.ComponentModel.DataAnnotations;

namespace OrderService.Web.Endpoints.AdminEndpoints;

public class CreateUserRequest
{
  public const string Route = "/admin/users";


#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

  [Required]
  public string email { get; set; }
  [Required]
  public string phoneNumber { get; set; }
  [Required]
  public string password { get; set; }
  [Required]
  public string fullname { get; set; }
  [Required]
  public string address { get; set; }
  [Required]
  public string role { get; set; }
}
