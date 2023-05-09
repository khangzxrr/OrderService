using Ardalis.Specification;

namespace OrderService.Core.UserAggregate.Specifications;
public class UserByPhonenumberSpec : Specification<User>, ISingleResultSpecification
{
  public UserByPhonenumberSpec(string phonenumber)
  {
    Query
      .Where(u => u.phoneNumber == phonenumber);
  }
}
