using Ardalis.SmartEnum;

namespace OrderService.Core.ProductAggregate;
public class ProductResellStatus : SmartEnum<ProductResellStatus>
{

  public static readonly ProductResellStatus notForSale = new(nameof(notForSale), 0);

  public static readonly ProductResellStatus selling = new(nameof(selling), 1);

  public static readonly ProductResellStatus sold = new(nameof(sold), 2);


  public ProductResellStatus(string name, int value ) : base(name, value) { }
}
