using Ardalis.GuardClauses;
using OrderService.SharedKernel;
using OrderService.SharedKernel.Interfaces;

namespace OrderService.Core.UserAggregate;
public class Role : EntityBase, IAggregateRoot
{
  public string roleName { get; set; }

  public Role(string roleName)
  {
    this.roleName = Guard.Against.NullOrEmpty(roleName);
  }

}
