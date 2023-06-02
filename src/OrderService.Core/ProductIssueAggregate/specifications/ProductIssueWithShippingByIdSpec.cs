
using Ardalis.Specification;
using OrderService.Core.ProductReturnAggregate;

namespace OrderService.Core.ProductIssueAggregate.specifications;
public class ProductIssueWithShippingByIdSpec : Specification<ProductIssue>, ISingleResultSpecification
{

  public ProductIssueWithShippingByIdSpec(int id)
  {
    Query
      .Include(pr => pr.product)
        .ThenInclude(p => p.productCategory)
      .Include(pr => pr.issuePayments)
      .Include(pr => pr.issueStateTrackings)
      .Include(pr => pr.productIssueShipping)
      .Where(pr => pr.Id == id);

  }
}
