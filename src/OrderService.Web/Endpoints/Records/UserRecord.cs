using OrderService.Core.UserAggregate;

namespace OrderService.Web.Endpoints.Records;

public record UserRecord(int id, string address, string fullName, string email, string passwordHash, string phoneNumber, DateTime dateOfBirth, string roleName)
{
  public static UserRecord FromEntity(User user)
  {
    return new UserRecord(user.Id, user.address, user.fullName, user.email, user.passwordHash, user.phoneNumber, user.dateOfBirth, user.role.roleName);
  }
}
