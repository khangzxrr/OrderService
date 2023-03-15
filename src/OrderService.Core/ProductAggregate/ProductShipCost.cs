using Ardalis.GuardClauses;
using OrderService.SharedKernel;

namespace OrderService.Core.ProductAggregate;
public class ProductShipCost : EntityBase
{
  public float shipCost { get; set; }
  public float costPerWeight { get; set; }

  public string additionalCostCondition { get; set; }

  public int productCategoryId { get; set; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
  public ProductShipCost(float shipCost, float costPerWeight, string additionalCostCondition)
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
  {
    this.shipCost = Guard.Against.Negative(shipCost, nameof(shipCost));
    this.costPerWeight = Guard.Against.Negative(costPerWeight, nameof(costPerWeight));
    this.additionalCostCondition = Guard.Against.NullOrEmpty(additionalCostCondition);
  }

}
