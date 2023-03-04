using OrderService.SharedKernel;

namespace OrderService.Core.ContributorAggregate.Events;

public class ContributorDeletedEvent : DomainEventBase
{
  public int ContributorId { get; set; }

  public ContributorDeletedEvent(int contributorId)
  {
    ContributorId = contributorId;
  }
}
