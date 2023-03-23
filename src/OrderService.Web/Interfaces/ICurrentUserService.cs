namespace OrderService.Web.Interfaces;

public interface ICurrentUserService
{
  public string? UserId { get; }
  public int TryParseUserId();
}
