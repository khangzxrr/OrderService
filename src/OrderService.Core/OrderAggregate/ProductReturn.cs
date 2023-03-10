using Ardalis.GuardClauses;
using OrderService.SharedKernel;

namespace OrderService.Core.OrderAggregate;
public class ProductReturn : EntityBase
{
  public bool isExchangeForNewProduct { get; set; }
  public int returnQuantity { get; set; }
  public string returnReason { get; set; }
  public string customerMessage { get; set; }
  public string supplierMessage { get; set; }

  public ShippingStatus shippingStatus { get; set; }
  public int shippingEstimatedDay { get; set; }
  public float returnCost { get; set; }
  public DateTime returnDate { get; set; }

  private List<ReturnPayment> _returnPayments = new List<ReturnPayment>();
  public IReadOnlyCollection<ReturnPayment> returnPayments => _returnPayments.AsReadOnly();

  public ProductReturn(
    bool isExchangeForNewProduct, 
    int returnQuantity, 
    string returnReason, 
    string customerMessage, 
    string supplierMessage,
    ShippingStatus shippingStatus, 
    int shippingEstimatedDay,
    float returnCost, 
    DateTime returnDate)
  {
    this.isExchangeForNewProduct = isExchangeForNewProduct;
    this.returnQuantity = Guard.Against.Negative(returnQuantity);

    this.returnReason = Guard.Against.NullOrEmpty(returnReason);
    this.customerMessage = Guard.Against.NullOrEmpty(customerMessage);
    this.supplierMessage = Guard.Against.NullOrEmpty(supplierMessage);
    this.shippingStatus = Guard.Against.Null(shippingStatus);
    this.shippingEstimatedDay = Guard.Against.Negative(shippingEstimatedDay);
    this.returnCost = Guard.Against.Negative(returnCost);
    this.returnDate = Guard.Against.Null(returnDate);
  }

  public void AddReturnPayment(ReturnPayment returnPayment)
  {
    Guard.Against.Null(returnPayment);
    this._returnPayments.Add(returnPayment);
  }
}
