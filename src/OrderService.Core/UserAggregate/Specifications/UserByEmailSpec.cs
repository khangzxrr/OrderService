using Ardalis.Specification;

namespace OrderService.Core.UserAggregate.Specifications;
public class UserByEmailSpec: Specification<User>, ISingleResultSpecification
{
  public UserByEmailSpec(string email)
  {
    Query
      .Where(u => u.email == email);
  }
}
