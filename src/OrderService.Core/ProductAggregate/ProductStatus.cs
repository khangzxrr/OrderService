using Ardalis.SmartEnum;

namespace OrderService.Core.ProductAggregate;
public class ProductStatus : SmartEnum<ProductStatus>
{

  public static readonly ProductStatus notForSale = new(nameof(notForSale), 0);


  public static readonly ProductStatus selling = new(nameof(selling), 1);

  public static readonly ProductStatus sold = new(nameof(sold), 2);

  public static readonly ProductStatus disable = new(nameof(disable), 3);

  public ProductStatus(string name, int value ) : base(name, value) { }
}
