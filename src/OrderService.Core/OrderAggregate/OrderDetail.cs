using Ardalis.GuardClauses;
using OrderService.Core.ProductAggregate;
using OrderService.SharedKernel;

namespace OrderService.Core.OrderAggregate;
public class OrderDetail : EntityBase
{
 #pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
  public ProductHistory productHistory { get; private set; }

  public float additionalCost { get; private set; }

  public float processCost { get; private set; }
  public int quantity { get; private set; }

  private List<ProductReturn> _productReturns = new List<ProductReturn>();
  public IReadOnlyCollection<ProductReturn> productReturns => _productReturns;

  public void setProduct(ProductHistory product)
  {
    productHistory = Guard.Against.Null(product);
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
