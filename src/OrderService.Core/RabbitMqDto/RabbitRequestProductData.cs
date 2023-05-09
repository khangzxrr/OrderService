

namespace OrderService.Core.RabbitMqDto;
public class RabbitRequestProductData
{
  public string connectionId { get; set; }
  public string productUrl { get; set; }

  public RabbitRequestProductData(string connectionId, string productUrl)
  {
    this.connectionId = connectionId;
    this.productUrl = productUrl;
  }
}
