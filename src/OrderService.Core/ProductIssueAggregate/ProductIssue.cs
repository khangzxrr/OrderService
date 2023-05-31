using Ardalis.GuardClauses;
using Microsoft.IdentityModel.Tokens;
using OrderService.Core.ProductAggregate;
using OrderService.Core.UserAggregate;
using OrderService.SharedKernel;
using OrderService.SharedKernel.Interfaces;

namespace OrderService.Core.ProductReturnAggregate;
public class ProductIssue : EntityBase, IAggregateRoot
{
  public Product product { get; private set; }
  public User assignedEmployee { get; private set; }
  
  public string returnReason { get; set; }
  public string customerEmail { get; set; }
  public string customerFullname { get; set; }
  public string customerPhonenumber { get; set; }
  public DateTime returnDate { get; set; }

  public bool isWarranty { get; set; }
  public ProductIssueStatus status { get; private set; }
  public ProductIssueFinishStatus finishStatus { get; set; }

  public string series { get; set; }

  private List<IssueMedia> _issueMedias = new List<IssueMedia>();
  public IReadOnlyCollection<IssueMedia> issueMedias => _issueMedias.AsReadOnly();


  private List<IssuePayment> _issuePayments = new List<IssuePayment>();
  public IReadOnlyCollection<IssuePayment> issuePayments => _issuePayments.AsReadOnly();

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
  public ProductIssue()
  {
    returnDate = DateTime.Now;
    isWarranty = false;
    status = ProductIssueStatus.request;
    finishStatus = ProductIssueFinishStatus.onGoing;
  }


  public void AssignEmployee(User user)
  {
    assignedEmployee = Guard.Against.Null(user);
  }

  public void AssignCustomerInfo(string customerEmail, string customerPhonenumber, string customerFullname)
  {
    this.customerEmail = Guard.Against.Null(customerEmail);
    this.customerFullname = Guard.Against.Null(customerFullname);
    this.customerPhonenumber = Guard.Against.Null(customerPhonenumber);
  }
  public void SetProduct(Product product)
  {
     this.product = Guard.Against.Null(product);
  }

  public void SetStatus(ProductIssueStatus status)
  {
    this.status = Guard.Against.Null(status);
  }

  public void AddReturnPayment(IssuePayment returnPayment)
  {
    Guard.Against.Null(returnPayment);
    _issuePayments.Add(returnPayment);
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
      var returnMedia = new IssueMedia(mediaUrl);
      _issueMedias.Add(returnMedia);
    }

  }

  public void SetFinishStatus(ProductIssueFinishStatus finishStatus)
  {
    this.finishStatus = Guard.Against.Null(finishStatus);
  }

  public void SetIsWarranty(bool isWarranty)
  {
    this.isWarranty = Guard.Against.Null(isWarranty);
  }
}
