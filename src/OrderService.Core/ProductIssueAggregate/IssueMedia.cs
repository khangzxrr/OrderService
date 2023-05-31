using Ardalis.GuardClauses;
using OrderService.SharedKernel;

namespace OrderService.Core.ProductReturnAggregate;
public class IssueMedia : EntityBase
{
  public string mediaUrl { get; set;  }

  public IssueMedia(string mediaUrl)
  {
    this.mediaUrl = Guard.Against.NullOrEmpty(mediaUrl);
  }
}
