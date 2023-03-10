using Ardalis.GuardClauses;
using OrderService.Core.UserAggregate;
using OrderService.SharedKernel;
using OrderService.SharedKernel.Interfaces;

namespace OrderService.Core.ChatAggregate;
public class Chat : EntityBase, IAggregateRoot
{
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
  public User? customer { get; set; }

  public User? employee { get; set; }

  private List<ChatMessage> _chatMessages = new List<ChatMessage>();
  public IReadOnlyCollection<ChatMessage> chatMessages => _chatMessages.AsReadOnly();

}
