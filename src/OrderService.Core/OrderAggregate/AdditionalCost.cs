using Ardalis.GuardClauses;
using OrderService.SharedKernel;
using OrderService.SharedKernel.Interfaces;

namespace OrderService.Core.OrderAggregate;
public class AdditionalCost : EntityBase, IAggregateRoot
{
  public string condition { get; set; }
  public float cost { get; set; }

  public AdditionalCost(string condition, float cost)
  {
    this.condition = Guard.Against.NullOrEmpty(condition);
    this.cost = Guard.Against.Negative(cost);
  }
}
