using System.ComponentModel.DataAnnotations;

namespace OrderService.Web.Endpoints.AuthenticationEndpoints;

public class RegisterRequest
{

  public const string Route = "/register";

  [Required]
  [MaxLength(100)]
  [MinLength(6)]
  public string Password { get; set; }
  [Required]
  [EmailAddress]
  public string Email { get; set; }

  [Required]
  [MaxLength(100)]
  [MinLength(9)]
  public string PhoneNumber { get; set; }

  [Required]
  [DataType(DataType.Date)]
  public DateTime? DateOfBirth { get; set; }
  [Required]
  public string Address { get; set; }
  [Required]
  public string FirstName { get; set; }
  [Required]
  public string LastName { get; set; }

  public RegisterRequest(string email, string phoneNumber, string password, DateTime? dateOfBirth, string address, string firstName, string lastName)
  {
    
    Password = password;
    Email = email;
    DateOfBirth = dateOfBirth;
    Address = address;
    FirstName = firstName;
    LastName = lastName;
    PhoneNumber = phoneNumber;
  }
}
