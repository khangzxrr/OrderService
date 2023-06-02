using Ardalis.Result;
using OrderService.Core.ProductReturnAggregate;

namespace OrderService.Core.Interfaces;
public interface IDetermineNextStateProductIssueService
{
  public Result<IEnumerable<ProductIssueStatus>> getNextStatesOf(ProductIssue productIssue);
}
