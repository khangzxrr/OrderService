using Ardalis.Result;
using OrderService.Core.UserAggregate;

namespace OrderService.Core.Interfaces;
public interface IAuthenticationService
{
  public Task<Result<User>> AuthenticationAsync(string email, string password);
  public Task<Result<User>> CreateNewUserAsync(string email, string phoneNumber, string password, string fullName, DateTime dateofbirth, string address);
}
