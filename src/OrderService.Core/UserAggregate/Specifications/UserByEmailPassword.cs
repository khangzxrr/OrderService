using Ardalis.Specification;

namespace OrderService.Core.UserAggregate.Specifications;
internal class UserByEmailPassword : Specification<User>, ISingleResultSpecification
{
  public UserByEmailPassword(string email, string password)
  {
    Query
      .Where(u => u.email == email && u.passwordHash == password)
      .Include(u => u.role);
  }
}
