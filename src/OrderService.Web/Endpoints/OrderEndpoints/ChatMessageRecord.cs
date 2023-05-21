using OrderService.Core.OrderAggregate;

namespace OrderService.Web.Endpoints.OrderEndpoints;

public record ChatMessageRecord(int id, bool isFromEmployee, string message, DateTime createdAt)
{
  public static ChatMessageRecord FromEntity(ChatMessage chatMessage)
  {
    return new ChatMessageRecord(chatMessage.Id, chatMessage.isFromEmployee, chatMessage.message, chatMessage.dateTime);
  }
}
