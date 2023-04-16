using Ardalis.Specification;

namespace OrderService.Core.ProductAggregate.Specifications;
public class ProductByIdSpec: Specification<Product>, ISingleResultSpecification
{
  public ProductByIdSpec(int productId)
  {
    Query
      .Where(p => p.Id == productId)
      .Include(p => p.currencyExchange)
      .Include(p => p.productCategory)
      .ThenInclude(pc => pc.productShipCost);
  }
}
