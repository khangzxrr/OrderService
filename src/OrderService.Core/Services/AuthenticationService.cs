using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Ardalis.GuardClauses;
using Ardalis.Result;
using OrderService.Core.Interfaces;
using OrderService.Core.UserAggregate;
using OrderService.Core.UserAggregate.Specifications;
using OrderService.SharedKernel.Interfaces;

namespace OrderService.Core.Services;
internal class AuthenticationService : IAuthenticationService
{

  private readonly IRepository<User> _userRepository;
  private readonly IRepository<Role> _roleRepository;

  public AuthenticationService(IRepository<User> userRepository, IRepository<Role> roleRepository)
  {
    _userRepository = userRepository;
    _roleRepository = roleRepository;
  }


  private string GenerateMD5(string text)
  {
    var hashPassword = "";

    using (MD5 hash = MD5.Create())
    {
      hashPassword = String.Join
      (
          "",
          from ba in hash.ComputeHash
          (
              Encoding.UTF8.GetBytes(text)
          )
          select ba.ToString("x2")
      );
    }

    return hashPassword;
  }

  public async Task<Result<User>> AuthenticationAsync(string email, string password)
  {
    if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
    {
      return Result.Error("email or password not must be null or empty");
    }

    var generatedHashPassword = GenerateMD5(password);

    var userSpec = new UserByEmailPassword(email, generatedHashPassword);
    var user = await _userRepository.FirstOrDefaultAsync(userSpec);

    if (user == null)
    {
      return Result.Error("Wrong email or password");
    }


    return Result.Success(user);
  }

  public async Task<Result<User>> CreateNewUserAsync(string email, string phoneNumber, string password, string fullName, string address)
  {
    Guard.Against.NullOrEmpty(email);
    Guard.Against.NullOrEmpty(password);
    Guard.Against.NullOrEmpty(phoneNumber);


    var userByEmailSpec = new UserByEmailSpec(email);
    var user = await _userRepository.FirstOrDefaultAsync(userByEmailSpec);

    if (user != null)
    {
      return Result.Error("email is exist");
    }

    var roleSpec = new RoleByNameSpec(RoleEnum.CUSTOMER);
    var role = await _roleRepository.FirstOrDefaultAsync(roleSpec);
    if (role == null)
    {
      return Result.Error("Role is not exist");
    }

    user = new User(email, phoneNumber, GenerateMD5(password), "salt", fullName, address);
    user.setRole(role!);

    await _userRepository.AddAsync(user);
    await _userRepository.SaveChangesAsync();
    
    return Result.Success(user);
  }

  public async Task<Result<User>> UpdateUser(int userId, string phoneNumber, string fullName, string address, string password)
  {
    var spec = new UserByIdSpec(userId);

    var user = await _userRepository.FirstOrDefaultAsync(spec);

    if (user == null)
    {
      return Result.Error("User not found");
    }

    user.SetPhoneNumber(phoneNumber);
    user.SetFullName(fullName);
    user.SetAdress(address);
    user.SetPassword(GenerateMD5(password));

    await _userRepository.SaveChangesAsync();

    return Result.Success(user);
  }
}
