using System.ComponentModel.DataAnnotations;

namespace OrderService.Web.Endpoints.AuthenticationEndpoints;

public class RegisterRequest
{

  public const string Route = "/register";

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.


  [Required]
  [MaxLength(100)]
  [MinLength(6)]
  public string Password { get; set; }
  [Required]
  [EmailAddress]
  public string Email { get; set; }

  [Required]
  [MaxLength(13)]
  [MinLength(9)]
  public string PhoneNumber { get; set; }

  [Required]
  [DataType(DataType.Date)]
  public DateTime? DateOfBirth { get; set; }
  [Required]
  public string Address { get; set; }
  [Required]
  public string FullName { get; set; }


}
