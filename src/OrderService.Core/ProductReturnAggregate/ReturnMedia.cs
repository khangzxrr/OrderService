using Ardalis.GuardClauses;
using OrderService.SharedKernel;

namespace OrderService.Core.ProductReturnAggregate;
public class ReturnMedia : EntityBase
{
  public string mediaUrl { get; set;  }

  public ReturnMedia(string mediaUrl)
  {
    this.mediaUrl = Guard.Against.NullOrEmpty(mediaUrl);
  }
}
