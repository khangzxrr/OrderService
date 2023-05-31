
using Ardalis.Specification;

namespace OrderService.Core.ProductReturnAggregate.specifications;
public class ProductReturnByIdSpec: Specification<ProductReturn>, ISingleResultSpecification
{
  public ProductReturnByIdSpec(int id)
  {
    Query
      .Include(pr => pr.product)
        .ThenInclude(p => p.productCategory)
      .Include(pr => pr.product)
        .ThenInclude(p => p.currencyExchange)
          .ThenInclude(ce => ce.currency)
      .Include(pr => pr.ReturnMedias)

      .Where(pr => pr.Id == id);
  }
}
