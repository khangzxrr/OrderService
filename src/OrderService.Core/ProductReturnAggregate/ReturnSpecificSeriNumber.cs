using Ardalis.GuardClauses;
using OrderService.SharedKernel;

namespace OrderService.Core.ProductReturnAggregate;
public class ReturnSpecificSeriNumber: EntityBase
{
  public string seriNumber { get; private set; }

  public ReturnSpecificSeriNumber(string seriNumber)
  {
    this.seriNumber = seriNumber;
  }

  public void SetSerialNumber(string serialNumber)
  {
    this.seriNumber = Guard.Against.NullOrEmpty(seriNumber);
  }
}
