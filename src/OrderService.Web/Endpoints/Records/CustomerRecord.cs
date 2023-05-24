using OrderService.Core.UserAggregate;

namespace OrderService.Web.Endpoints.Records;

public record CustomerRecord(int id, string fullname, string address, string phoneNumber, string email, string verify, int totalOrdersCount, double totalPaymentAmount)
{
  public static CustomerRecord FromEntity(User user, int totalOrdersCount, double totalPaymentAmount)
  {
    return new CustomerRecord(user.Id, user.fullname, user.address, user.phoneNumber, user.email, user.verify.Name, totalOrdersCount, totalPaymentAmount);
  }
}
