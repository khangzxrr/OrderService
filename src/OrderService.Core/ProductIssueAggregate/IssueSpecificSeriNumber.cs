using Ardalis.GuardClauses;
using OrderService.SharedKernel;

namespace OrderService.Core.ProductReturnAggregate;
public class IssueSpecificSeriNumber: EntityBase
{
  public string seriNumber { get; private set; }

  public IssueSpecificSeriNumber(string seriNumber)
  {
    this.seriNumber = seriNumber;
  }

  public void SetSerialNumber(string serialNumber)
  {
    this.seriNumber = Guard.Against.NullOrEmpty(seriNumber);
  }
}
