using OrderService.SharedKernel;
using OrderService.SharedKernel.Interfaces;

namespace OrderService.Core.ProductAggregate;
public class ProductTax : EntityBase, IAggregateRoot
{
  public string taxName { get; set; }
  public float taxPrice { get; set; }

  private List<Product> _products = new List<Product>();
  public IReadOnlyCollection<Product> products => _products.AsReadOnly();


  private List<ProductHistory> _productHistories = new List<ProductHistory>();
  public IReadOnlyCollection<ProductHistory> productHistories => _productHistories.AsReadOnly();

  public ProductTax(string taxName, float taxPrice)
  {
    this.taxName = taxName;
    this.taxPrice = taxPrice;
  }
}
