
using Ardalis.GuardClauses;
using OrderService.Core.ProductReturnAggregate;
using OrderService.SharedKernel;

namespace OrderService.Core.ProductIssueAggregate;
public class IssueStateTracking : EntityBase
{
  public ProductIssueStatus productIssueStatus { get; private set; }
  public DateTime changeDate { get; private set; }

  public IssueStateTracking(ProductIssueStatus productIssueStatus)

  {
    this.changeDate = DateTime.Now;
    this.productIssueStatus = productIssueStatus;
  }

  public void SetProductIssueStatus(ProductIssueStatus productIssueStatus)
  {
    this.productIssueStatus = Guard.Against.Null(productIssueStatus);
  }
}
