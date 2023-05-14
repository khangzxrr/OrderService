using Ardalis.GuardClauses;
using OrderService.Core.UserAggregate;
using OrderService.SharedKernel;

namespace OrderService.Core.OrderAggregate;
public class Chat : EntityBase
{
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
  public User employee { get; private set; }

  private readonly List<ChatMessage> _chatMessages = new();
  public IEnumerable<ChatMessage> chatMessages => _chatMessages.AsReadOnly();

  public void SetEmployee(User employee)
  {
    this.employee = Guard.Against.Null(employee);
  }

  public void AddMessageFromEmployee(string message)
  {
    Guard.Against.NullOrEmpty(message);

    ChatMessage chatMessage = new ChatMessage(true, message);
    _chatMessages.Add(chatMessage);
  }

  public void AddMessageFromCustomer(string message)
  {
    Guard.Against.NullOrEmpty(message);

    ChatMessage chatMessage = new ChatMessage(false, message);
    _chatMessages.Add(chatMessage);
  }



}
