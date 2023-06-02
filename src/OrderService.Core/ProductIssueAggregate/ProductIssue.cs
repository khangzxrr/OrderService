using Ardalis.GuardClauses;
using OrderService.Core.ProductAggregate;
using OrderService.Core.ProductIssueAggregate;
using OrderService.Core.UserAggregate;
using OrderService.SharedKernel;
using OrderService.SharedKernel.Interfaces;

namespace OrderService.Core.ProductReturnAggregate;
public class ProductIssue : EntityBase, IAggregateRoot
{
  public Product product { get; private set; }
  public User assignedEmployee { get; private set; }
  
  public float totalOrderDetailPrice { get; private set; }
  public string returnReason { get; set; }
  public string customerEmail { get; set; }
  public string customerFullname { get; set; }
  public string customerPhonenumber { get; set; }
  public string customerAddress { get; set; }

  public DateTime returnDate { get; set; }

  public bool isWarranty { get; set; }
  public ProductIssueStatus status { get; private set; }

  public string series { get; set; }

  private List<IssueMedia> _issueMedias = new List<IssueMedia>();
  public IReadOnlyCollection<IssueMedia> issueMedias => _issueMedias.AsReadOnly();


  private List<IssuePayment> _issuePayments = new List<IssuePayment>();
  public IReadOnlyCollection<IssuePayment> issuePayments => _issuePayments.AsReadOnly();


  private List<IssueStateTracking> _issueStateTrackings = new List<IssueStateTracking>();
  public IReadOnlyCollection<IssueStateTracking> issueStateTrackings => _issueStateTrackings.AsReadOnly();

  public ProductIssueShipping? productIssueShipping { get; private set; }


#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
  public ProductIssue(float totalOrderDetailPrice, bool isWarranty, string series, string returnReason, string customerEmail, string customerFullname, string customerPhonenumber, string customerAddress)
  {
    
    this.isWarranty = Guard.Against.Null(isWarranty);
    this.series = Guard.Against.Null(series);
    this.returnReason = Guard.Against.NullOrEmpty(returnReason);
    this.customerEmail = Guard.Against.NullOrEmpty(customerEmail);
    this.customerFullname = Guard.Against.NullOrEmpty(customerFullname);
    this.customerPhonenumber = Guard.Against.NullOrEmpty(customerPhonenumber);
    this.customerAddress = Guard.Against.NullOrEmpty(customerAddress);

    this.totalOrderDetailPrice = Guard.Against.Negative(totalOrderDetailPrice);

    returnDate = DateTime.Now;

    SetStatus(ProductIssueStatus.request);

  }

  private void AddTrackingState(ProductIssueStatus productIssueStatus)
  {
    _issueStateTrackings.Add(new IssueStateTracking(productIssueStatus));
  }

  public void AssignShipping(ProductIssueShipping shipping)
  {
    productIssueShipping = Guard.Against.Null(shipping);
  }

  public void AssignEmployee(User user)
  {
    assignedEmployee = Guard.Against.Null(user);
  }

  public void SetProduct(Product product)
  {
     this.product = Guard.Against.Null(product);
  }

  public void SetStatus(ProductIssueStatus status)
  {
    this.status = Guard.Against.Null(status);
    AddTrackingState(status);
  }

  public void AcceptAllIssuePayments()
  {
    foreach(var payment in _issuePayments)
    {
      payment.setIsPaid(true);
    }
  }

  public void AddIssuePayment(IssuePayment returnPayment)
  {
    Guard.Against.Null(returnPayment);
    _issuePayments.Add(returnPayment);
  }

  public void SetMedias(string[] medias)
  {

    foreach(string mediaUrl in medias)
    {
      var returnMedia = new IssueMedia(mediaUrl);
      _issueMedias.Add(returnMedia);
    }

  }

}
