using Ardalis.GuardClauses;
using OrderService.SharedKernel;

namespace OrderService.Core.OrderAggregate;
public class OrderDetail : EntityBase
{
 #pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
  public ProductHistory productHistory { get; private set; }

  public float additionalCost { get; private set; }
  public float shipCost { get; private set; }
  public float productCost { get; private set; }
  public float processCost { get; private set; }
  public int quantity { get; private set; }

  private readonly List<ProductReturn> _productReturns = new List<ProductReturn>();
  public IEnumerable<ProductReturn> productReturns => _productReturns.AsReadOnly();

  public void setProduct(ProductHistory product)
  {
    Guard.Against.Null(product.productCategory);

    productHistory = Guard.Against.Null(product);
    productCost = Guard.Against.Negative(productHistory.productPrice);
    shipCost = Guard.Against.Negative(productHistory.productCategory.productShipCost.shipCost);
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

}
