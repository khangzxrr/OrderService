

namespace OrderService.Core.RabbitMqDto;
public class RabbitRequestProductData
{
  public int userId { get; set; }
  public string productUrl { get; set; }

  public RabbitRequestProductData(int userId, string productUrl)
  {
    this.userId = userId;
    this.productUrl = productUrl;
  }
}
