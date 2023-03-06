using System;
using Ardalis.SmartEnum;

namespace OrderService.Core.UserAggregate
{
  public class UserRole: SmartEnum<UserRole>
  {
    public static readonly UserRole admin = new(nameof(admin), 1);
    public static readonly UserRole manager = new(nameof(manager), 2);
    public static readonly UserRole employee = new(nameof(employee), 3);
    public static readonly UserRole customer = new(nameof(customer), 4);


    public UserRole(string rolename, int roleid) : base(rolename, roleid)
    {
    }
  }
}

