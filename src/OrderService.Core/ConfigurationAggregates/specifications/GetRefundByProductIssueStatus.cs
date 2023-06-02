
using Ardalis.Specification;
using OrderService.Core.ProductReturnAggregate;

namespace OrderService.Core.ProductIssueRefundConfiguration.specifications;
public class GetRefundByProductIssueStatus : Specification<ProductIssueRefundConfiguration>, ISingleResultSpecification
{
  public GetRefundByProductIssueStatus(ProductIssueStatus productIssueStatus)
  {
    Query
      .Where(refund => refund.productIssueStatus == productIssueStatus);
  }
}
