using OrderService.Core.UserAggregate;

namespace OrderService.Web.Endpoints.Records;

public record UserRecord(int id, string address, string firstName, string lastName, string email, string passwordHash, string phoneNumber, DateTime dateOfBirth, string roleName)
{
  public static UserRecord FromEntity(User user)
  {
    return new UserRecord(user.Id, user.address, user.firstname, user.lastname, user.email, user.passwordHash, user.phoneNumber, user.dateOfBirth, user.role.roleName);
  }
}
