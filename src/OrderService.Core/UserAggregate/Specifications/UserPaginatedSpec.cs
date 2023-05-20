
using Ardalis.Specification;

namespace OrderService.Core.UserAggregate.Specifications;
public class UserPaginatedSpec : Specification<User>
{
  public UserPaginatedSpec(int skip, int take, string? roleName)
  {
    Query
      .Include(u => u.role)
      .Where(u => u.role.roleName == roleName, roleName != null)
      .Skip(skip)
      .Take(take);
  }
}
