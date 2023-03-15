using System.Security.Claims;
using OrderService.Web.Interfaces;

namespace OrderService.Web.Services;

public class CurrentUserService : ICurrentUserService
{
  private readonly IHttpContextAccessor _contextAccessor;

  public CurrentUserService(IHttpContextAccessor httpContextAccessor)
  {
    _contextAccessor = httpContextAccessor;
  }


  public string? UserId => _contextAccessor.HttpContext!.User.Claims.Where(c => c.Type == "userId").FirstOrDefault()?.Value;


  public int TryParseUserId()
  {
    return int.Parse(UserId!);
  }
}
