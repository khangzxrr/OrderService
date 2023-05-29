using Ardalis.GuardClauses;
using OrderService.Core.OrderAggregate;
using OrderService.SharedKernel;
using OrderService.SharedKernel.Interfaces;

namespace OrderService.Core.ProductReturnAggregate;
public class ProductReturn : EntityBase, IAggregateRoot
{
  public int orderDetailId { get; set; }
  public string returnReason { get; set; }
  public DateTime returnDate { get; set; }

  public ProductReturnStatus status { get; private set; }
  
  public bool isFinished { get; set; }

  private List<ReturnSpecificSeriNumber> _returnSpecificSeriNumbers = new List<ReturnSpecificSeriNumber>();
  public IReadOnlyCollection<ReturnSpecificSeriNumber> returnSpecificSeriNumbers => _returnSpecificSeriNumbers;

  private List<ReturnMedia> _returnMedias = new List<ReturnMedia>();
  public IReadOnlyCollection<ReturnMedia> ReturnMedias => _returnMedias.AsReadOnly();

  private List<ReturnPayment> _returnPayments = new List<ReturnPayment>();
  public IReadOnlyCollection<ReturnPayment> returnPayments => _returnPayments.AsReadOnly();

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
  private ProductReturn()
  {

  }

#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

  public ProductReturn(
    string returnReason,
    DateTime returnDate
    )
  {

    this.returnReason = Guard.Against.NullOrEmpty(returnReason);
    this.returnDate = Guard.Against.Null(returnDate);

    isFinished = false;

    status = ProductReturnStatus.verifying;
  }

  public void SetStatus(ProductReturnStatus status)
  {
    this.status = Guard.Against.Null(status);
  }

  public void AddReturnPayment(ReturnPayment returnPayment)
  {
    Guard.Against.Null(returnPayment);
    _returnPayments.Add(returnPayment);
  }
}
