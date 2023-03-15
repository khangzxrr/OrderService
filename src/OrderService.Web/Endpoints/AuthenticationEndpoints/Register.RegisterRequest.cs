﻿using System.ComponentModel.DataAnnotations;

namespace OrderService.Web.Endpoints.AuthenticationEndpoints;

public class RegisterRequest
{

  public const string Route = "/register";

  [Required]
  [MaxLength(100)]
  [MinLength(6)]
  public string Password { get; set; }
  [Required]
  [DataType(DataType.EmailAddress)]
  public string Email { get; set; }

  [Required]
  public DateTime DateOfBirth { get; set; }
  [Required]
  public string Address { get; set; }
  [Required]
  public string FirstName { get; set; }
  [Required]
  public string LastName { get; set; }

  public RegisterRequest(string email,string password, DateTime dateOfBirth, string address, string firstName, string lastName)
  {
    
    Password = password;
    Email = email;
    DateOfBirth = dateOfBirth;
    Address = address;
    FirstName = firstName;
    LastName = lastName;
  }
}
