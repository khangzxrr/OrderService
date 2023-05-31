using Ardalis.GuardClauses;
using Microsoft.IdentityModel.Tokens;
using OrderService.Core.ProductAggregate;
using OrderService.SharedKernel;
using OrderService.SharedKernel.Interfaces;

namespace OrderService.Core.ProductReturnAggregate;
public class ProductReturn : EntityBase, IAggregateRoot
{
  public Product product { get; set; }

  public string returnReason { get; set; }
  public DateTime returnDate { get; set; }

  public bool isWarranty { get; set; }
  public ProductReturnStatus status { get; private set; }
  public ProductReturnFinishStatus finishStatus { get; set; }

  public string series { get; set; }

  private List<ReturnMedia> _returnMedias = new List<ReturnMedia>();
  public IReadOnlyCollection<ReturnMedia> ReturnMedias => _returnMedias.AsReadOnly();


  private List<ReturnPayment> _returnPayments = new List<ReturnPayment>();
  public IReadOnlyCollection<ReturnPayment> returnPayments => _returnPayments.AsReadOnly();

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
  public ProductReturn()
  {
    returnDate = DateTime.Now;
    isWarranty = false;
    status = ProductReturnStatus.request;
    finishStatus = ProductReturnFinishStatus.onGoing;
  }


  public void SetProduct(Product product)
  {
     this.product = Guard.Against.Null(product);
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

  public void SetReturnReason(string? description)
  {
    returnReason = description.IsNullOrEmpty() ? "" : description!;
  }
  public void SetSeries(string? series)
  {
    this.series = series.IsNullOrEmpty() ? "" : series!;
  }

  public void SetMedias(string[] medias)
  {

    foreach(string mediaUrl in medias)
    {
      var returnMedia = new ReturnMedia(mediaUrl);
      _returnMedias.Add(returnMedia);
    }

  }

  public void SetIsWarranty(bool isWarranty)
  {
    this.isWarranty = Guard.Against.Null(isWarranty);
  }
}
