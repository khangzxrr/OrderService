
using Ardalis.Specification;

namespace OrderService.Core.ProductReturnAggregate.specifications;
public class ProductReturnByProductIdSpec : Specification<ProductReturn>, ISingleResultSpecification
{
  public ProductReturnByProductIdSpec(int productId)
  {
    Query
      .Include(pr => pr.product)
      .Where(pr => pr.product.Id == productId)
      .Where(pr => pr.finishStatus == ProductReturnFinishStatus.onGoing);

  }
}
