using Ardalis.Specification;

namespace OrderService.Core.UserAggregate.Specifications;
public class RoleByNameSpec : Specification<Role>, ISingleResultSpecification
{
  public RoleByNameSpec(RoleEnum role)
  {
    Query
      .Where(r => r.roleName == role.ToString());
  }
}
