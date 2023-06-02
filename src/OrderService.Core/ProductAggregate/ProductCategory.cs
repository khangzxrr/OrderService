using Ardalis.GuardClauses;
using OrderService.SharedKernel;
using OrderService.SharedKernel.Interfaces;

namespace OrderService.Core.ProductAggregate;
public class ProductCategory : EntityBase, IAggregateRoot
{
  public string productCategoryName { get; } 
 public ProductCategory(string productCategoryName)
  {
    this.productCategoryName = Guard.Against.NullOrEmpty(productCategoryName, nameof(productCategoryName));
  } 


}
