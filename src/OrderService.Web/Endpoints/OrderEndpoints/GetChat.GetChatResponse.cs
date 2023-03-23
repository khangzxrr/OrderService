namespace OrderService.Web.Endpoints.OrderEndpoints;

public class GetChatResponse
{
  public IEnumerable<ChatMessageRecord> chatMessages { get; set; } = new List<ChatMessageRecord>();

  public GetChatResponse(IEnumerable<ChatMessageRecord> chatMessages)
  {
    this.chatMessages = chatMessages;
  }
}
