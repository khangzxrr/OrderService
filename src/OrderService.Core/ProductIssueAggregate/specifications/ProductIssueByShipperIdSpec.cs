
using Ardalis.Specification;
using OrderService.Core.ProductReturnAggregate;

namespace OrderService.Core.ProductIssueAggregate.specifications;
public class ProductIssueByShipperIdSpec : Specification<ProductIssue>
{
  public ProductIssueByShipperIdSpec(int shipperId)
  {
    Query
      .Include(pr => pr.product)
        .ThenInclude(p => p.productCategory)
      .Include(pr => pr.issuePayments)
      .Include(pr => pr.issueStateTrackings)
      .Include(pr => pr.productIssueShipping)

      .Where(pr => (pr.productIssueShipping != null) && (pr.productIssueShipping.shipper.Id == shipperId));


  }
}
