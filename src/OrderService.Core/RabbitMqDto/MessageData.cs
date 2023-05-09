namespace OrderService.Core.RabbitMqDto;
public class MessageData
{
  public string connectionId { get; set; }
  public string message { get; set; }

  public MessageData(string connectionId,  string message)
  {
    this.connectionId = connectionId;
    this.message = message;
  }
}
