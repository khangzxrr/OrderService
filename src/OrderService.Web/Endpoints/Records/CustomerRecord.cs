using OrderService.Core.UserAggregate;

namespace OrderService.Web.Endpoints.Records;

public record CustomerRecord(int id, string fullname, string address, string phoneNumber, string email, string verify)
{
  public static CustomerRecord FromEntity(User user)
  {
    return new CustomerRecord(user.Id, user.fullname, user.address, user.phoneNumber, user.email, user.verify.Name);
  }
}
