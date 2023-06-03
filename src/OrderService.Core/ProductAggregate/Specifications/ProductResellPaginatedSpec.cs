using Ardalis.Specification;

namespace OrderService.Core.ProductAggregate.Specifications;
public class ProductResellPaginatedSpec : Specification<Product>
{
  public ProductResellPaginatedSpec(int take, int skip, ProductStatus productResellStatus)
  {
    Query
      .Include(p => p.productCategory)
      .Include(p => p.currencyExchange)
        .ThenInclude(ce => ce.currency)

      .Where(p => p.productStatus == productResellStatus)
      .Take(take)
      .Skip(skip);
  }
}
