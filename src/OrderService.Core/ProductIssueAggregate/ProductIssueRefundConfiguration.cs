using OrderService.Core.ProductReturnAggregate;
using OrderService.SharedKernel;
using OrderService.SharedKernel.Interfaces;

namespace OrderService.Core.ProductIssueAggregate;
public class ProductIssueRefundConfiguration : EntityBase, IAggregateRoot
{
  public ProductIssueStatus productIssueStatus { get; private set; }
  public float refundRate { get; private set; }

  public ProductIssueRefundConfiguration(ProductIssueStatus productIssueStatus, float refundRate)
  {
    this.productIssueStatus = productIssueStatus;
    this.refundRate = refundRate;
  }


}
