using OrderService.Web.Endpoints.OrderEndpoints;

namespace OrderService.Web.Endpoints.EmployeeEndpoints;

public class GetChatByEmployeeResponse
{
  public IEnumerable<ChatMessageRecord> chatMessages { get; set; }

  public GetChatByEmployeeResponse(IEnumerable<ChatMessageRecord> chatMessages)
  {
    this.chatMessages = chatMessages;
  }
}
