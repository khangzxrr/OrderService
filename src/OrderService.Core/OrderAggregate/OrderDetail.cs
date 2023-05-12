using Ardalis.GuardClauses;
using OrderService.Core.ProductAggregate;
using OrderService.SharedKernel;

namespace OrderService.Core.OrderAggregate;
public class OrderDetail : EntityBase
{
 #pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
  public Product product { get; private set; }

  public float additionalCost { get; private set; }
  public float shipCost { get; private set; }
  public float processCost { get; private set; }

  public float totalCost { get; private set; }
  public int quantity { get; private set; }

  private readonly List<ProductReturn> _productReturns = new List<ProductReturn>();
  public IEnumerable<ProductReturn> productReturns => _productReturns.AsReadOnly();

  public void setProduct(Product product)
  {
    Guard.Against.Null(product.productCategory);

    this.product = Guard.Against.Null(product);
    shipCost = Guard.Against.Negative(product.productCategory.productShipCost.shipCost);

  }

  public void setAdditionalCost(float additionalCost)
  {
    this.additionalCost = Guard.Against.Negative(additionalCost);
  }

  public void setProcessCost(float processCost)
  {
    this.processCost = Guard.Against.Negative(processCost);
  }

  public void setQuantity(int quantity)
  {
    this.quantity = Guard.Against.NegativeOrZero(quantity);
  }

  public void setTotalCost(float totalCost)
  {
    this.totalCost = Guard.Against.Negative(totalCost);
  }
}
