namespace OrderService.Core.RabbitMqDto;
public class MessageData
{
  public int userId { get; set; }
  public string message { get; set; }

  public MessageData(int userId,  string message)
  {
    this.userId = userId;
    this.message = message;
  }
}
