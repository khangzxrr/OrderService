namespace OrderService.Core.RabbitMqDto;
public class RabbitResponseProductData
{
  public string connectionId { get; set; }
  public string imageUrl { get; set; }
  public string catalog { get; set; }
  public string product { get; set; }
  public double price { get; set; }
  public int shipDays { get; set; }
  public double shipCost { get; set; }
  public string url { get; set; }
  public int returnDays { get; set; }


  public RabbitResponseProductData(string connectionId, string imageUrl, string catalog, string product, double price, int shipDays, double shipCost, string url, int returnDays)
  {
    this.connectionId = connectionId;

    this.imageUrl = imageUrl;
    this.catalog = catalog;
    this.product = product;
    this.price = price;
    this.shipDays = shipDays;
    this.url = url;
    this.returnDays = returnDays;
    this.shipCost = shipCost;
  }
}
