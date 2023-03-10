using Ardalis.GuardClauses;
using OrderService.SharedKernel;
using OrderService.SharedKernel.Interfaces;

namespace OrderService.Core.ProductAggregate;
public class ProductShipCost : EntityBase
{
  public float cost { get; set; }

  public int productCategoryId { get; set; }
  public ProductCategory productCategory { get; set; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
  public ProductShipCost(float cost)
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
  {
    this.cost = Guard.Against.Negative(cost, nameof(cost));
  }

  public void setProductCategory(ProductCategory category)
  {
    this.productCategory = Guard.Against.Null(category);
  }
}
