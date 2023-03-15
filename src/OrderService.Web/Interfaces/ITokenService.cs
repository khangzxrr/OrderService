using OrderService.Core.UserAggregate;

namespace OrderService.Web.Interfaces;

public interface ITokenService
{
  public string GenerateToken(User user);
}
