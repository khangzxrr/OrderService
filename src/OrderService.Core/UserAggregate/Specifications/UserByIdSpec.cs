using Ardalis.Specification;

namespace OrderService.Core.UserAggregate.Specifications;
public class UserByIdSpec : Specification<User>, ISingleResultSpecification
{
  public UserByIdSpec(int userId)
  {
    Query
      .Where(u => u.Id == userId)
        .Include(u => u.orders);
  }
}
