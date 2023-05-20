

namespace OrderService.Core.RabbitMqDto;
public class RabbitRequestProductData
{
  public string connectionId { get; set; }
  public string productUrl { get; set; }

  public int productQuantity { get; set; }

  public RabbitRequestProductData(string connectionId, string productUrl, int productQuantity)
  {
    this.connectionId = connectionId;
    this.productUrl = productUrl;
    this.productQuantity = productQuantity;
  }
}
