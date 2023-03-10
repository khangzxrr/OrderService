using Ardalis.GuardClauses;
using OrderService.Core.ProductAggregate;
using OrderService.SharedKernel;
using OrderService.SharedKernel.Interfaces;

namespace OrderService.Core.OrderAggregate;
public class OrderDetail : EntityBase
{
 #pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
  public ProductHistory product { get; private set; }

  public AdditionalCost additionalCost { get; private set; }

  private List<ProductReturn> _productReturns = new List<ProductReturn>();
  public IReadOnlyCollection<ProductReturn> productReturns => _productReturns;
  public float processCost { get; private set; }
  public int quantity { get; private set; }

  public void setProduct(ProductHistory product)
  {
    this.product = Guard.Against.Null(product);
  }

  public void setAdditionalCost(AdditionalCost additionalCost)
  {
    this.additionalCost = Guard.Against.Null(additionalCost);
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
