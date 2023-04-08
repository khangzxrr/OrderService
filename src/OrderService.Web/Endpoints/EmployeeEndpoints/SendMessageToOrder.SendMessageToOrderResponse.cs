using OrderService.Web.Endpoints.OrderEndpoints;

namespace OrderService.Web.Endpoints.EmployeeEndpoints;

public class SendMessageToOrderResponse
{
  public IEnumerable<ChatMessageRecord> chatMessages { get; set; }

  public SendMessageToOrderResponse(IEnumerable<ChatMessageRecord> chatMessages)
  {
    this.chatMessages = chatMessages;
  }
}
