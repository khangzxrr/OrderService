namespace OrderService.Web.Interfaces;

public interface ICurrentUserService
{
  string? UserName { get; }
  string? BirdOwnerId { get; }
  string? UserId { get; }

  public int TryParseBirdOwnerId();
  public int TryParseUserId();  
}
