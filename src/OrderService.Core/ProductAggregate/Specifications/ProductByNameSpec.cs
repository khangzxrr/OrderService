using Ardalis.Specification;

namespace OrderService.Core.ProductAggregate.Specifications;
public class ProductByNameSpec: Specification<Product>, ISingleResultSpecification
{
  public ProductByNameSpec(string name)
  {
    Query
      .Where(p => p.productName == name)
      .Include(p => p.productCategory);
  }
}
