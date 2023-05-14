using Ardalis.GuardClauses;
using OrderService.Core.OrderAggregate;
using OrderService.Core.ShipperAggregate;
using OrderService.SharedKernel;
using OrderService.SharedKernel.Interfaces;

namespace OrderService.Core.UserAggregate;

public class User: EntityBase, IAggregateRoot
{
  public string guid { get; private set; }
  public string email { get; private set; }
  public string phoneNumber { get; private set; }
  public string passwordHash { get; private set; }
  public string passwordSalt { get; private set; }
  public string firstname { get; private set; }
  public string lastname { get; private set; }
  public DateTime dateOfBirth { get; private set; }
  public string address { get; private set; }

  public UserVerify verify { get; }
  public Role role { get; private set; } = null!; //role is not null, regardless of senceario

  private List<Order> _orders = new List<Order>();
  public IReadOnlyCollection<Order> orders => _orders.AsReadOnly();

  public Shipper? shipper { get; private set; }

  public User(
    string email,
    string phoneNumber,
    string passwordHash,
    string passwordSalt,
    string firstname,
    string lastname,
    DateTime dateOfBirth,
    string address
    )
  {
    this.email = Guard.Against.NullOrEmpty(email);
    this.phoneNumber = Guard.Against.NullOrEmpty(phoneNumber);
    this.passwordHash = Guard.Against.NullOrEmpty(passwordHash, nameof(passwordHash));
    this.passwordSalt = Guard.Against.NullOrEmpty(passwordSalt, nameof(passwordSalt));
    this.firstname = Guard.Against.NullOrEmpty(firstname, nameof(firstname));
    this.lastname = Guard.Against.NullOrEmpty(lastname, nameof(lastname));
    this.dateOfBirth = Guard.Against.OutOfSQLDateRange(dateOfBirth, nameof(dateOfBirth));
    this.address = Guard.Against.NullOrEmpty(address, nameof(address));

    this.guid = "NOT_USING_GOOGLE";

    this.verify = UserVerify.actived;
  }

  public string getFullname()
  {
    return firstname + " " + lastname;
  }

  public void setRole(Role role)
  {
    this.role = Guard.Against.Null(role);
  }


  public void addOrder(Order order)
  {
    Guard.Against.Null(order);
    _orders.Add(order);
  }
}

