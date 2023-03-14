using Ardalis.GuardClauses;
using OrderService.SharedKernel;
using OrderService.SharedKernel.Interfaces;

namespace OrderService.Core.ProductAggregate;
public class ProductCategory : EntityBase, IAggregateRoot
{
  public string productCategoryName { get; } 

  public ProductShipCost productShipCost { get; private set; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
  public ProductCategory(string productCategoryName)
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
  {
    this.productCategoryName = Guard.Against.NullOrEmpty(productCategoryName, nameof(productCategoryName));
  } 

  public void SetProductShipCost(ProductShipCost productShipCost)
  {
    this.productShipCost = Guard.Against.Null(productShipCost);
  }

}
