
using Ardalis.Specification;

namespace OrderService.Core.ProductReturnAggregate.specifications;
public class ProductIssueByIdSpec: Specification<ProductIssue>, ISingleResultSpecification
{
  public ProductIssueByIdSpec(int id)
  {
    Query
      .Include(pr => pr.product)
        .ThenInclude(p => p.productCategory)
      .Include(pr => pr.product)
        .ThenInclude(p => p.currencyExchange)
          .ThenInclude(ce => ce.currency)
      .Include(pr => pr.issueMedias)
      .Include(pr => pr.issuePayments)
      .Include(pr => pr.issueStateTrackings)

      .Where(pr => pr.Id == id);
  }
}
