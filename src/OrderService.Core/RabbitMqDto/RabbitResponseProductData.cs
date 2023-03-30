namespace OrderService.Core.RabbitMqDto;
public class RabbitResponseProductData
{
  public int UserId { get; set; }
  public string Catalog { get; set; }
  public string Product { get; set; }
  public string Price { get; set; }
  public string Ship { get; set; }

  public string Url { get; set; }

  public RabbitResponseProductData(int userId, string catalog, string product, string price, string ship, string url)
  {
    UserId = userId;
    Catalog = catalog;
    Product = product;
    Price = price;
    Ship = ship;
    Url = url;
  }
}
