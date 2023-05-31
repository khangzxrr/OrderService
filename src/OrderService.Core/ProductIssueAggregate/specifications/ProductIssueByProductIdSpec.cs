
using Ardalis.Specification;

namespace OrderService.Core.ProductReturnAggregate.specifications;
public class ProductIssueByProductIdSpec : Specification<ProductIssue>, ISingleResultSpecification
{
  public ProductIssueByProductIdSpec(int productId)
  {
    Query
      .Include(pr => pr.product)
      .Where(pr => pr.product.Id == productId)
      .Where(pr => pr.finishStatus == ProductIssueFinishStatus.onGoing);

  }
}
