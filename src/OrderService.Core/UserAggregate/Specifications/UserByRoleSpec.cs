using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ardalis.Specification;

namespace OrderService.Core.UserAggregate.Specifications;
public class UserByRoleSpec: Specification<User>, ISingleResultSpecification
{
  public UserByRoleSpec(RoleEnum role)
  {
    Query
      .Include(u => u.role)
      .Where(u => u.role.roleName == role.ToString());
  }
}
