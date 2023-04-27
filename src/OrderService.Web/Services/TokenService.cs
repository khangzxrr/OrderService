using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using OrderService.Core.UserAggregate;
using OrderService.Web.Interfaces;

namespace OrderService.Web.Services;

public class TokenService : ITokenService
{
  private readonly IConfiguration _configuration;
  
  public TokenService(IConfiguration configuration)
  {
    _configuration = configuration;
  }


  public string GenerateToken(User user)
  {
    string key = _configuration["Jwt:Key"]!;
    string issuer = _configuration["Jwt:Issuer"]!;
    string audience = _configuration["Jwt:Audience"]!;

    var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
    var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

    var claims = new[]
    {
      new Claim(ClaimTypes.Role, user.role.roleName),
      new Claim("userId", user.Id.ToString())
    };

    var token = new JwtSecurityToken(
     issuer,
     audience,
     claims,
     expires: DateTime.Now.AddDays(180),
     signingCredentials: credentials);

    return new JwtSecurityTokenHandler().WriteToken(token);
  }
}
