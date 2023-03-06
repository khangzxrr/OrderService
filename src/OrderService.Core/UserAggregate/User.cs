
using Ardalis.GuardClauses;
using OrderService.SharedKernel;
using OrderService.SharedKernel.Interfaces;

namespace OrderService.Core.UserAggregate
{
  public class User: EntityBase, IAggregateRoot
  {
    public int userId { get; }
    public string guid { get; }
    public string passwordHash { get; private set; }
    public string passwordSalt { get; private set; }
    public string firstname { get; private set; }
    public string lastname { get; private set; }
    public DateTime dateOfBirth { get; private set; }
    public string address { get; private set; }

    public UserVerify verify { get; }
    public UserRole role { get; }
    public User(
      int userId,
      string guid,
      string passwordHash,
      string passwordSalt,
      string firstname,
      string lastname,
      DateTime dateOfBirth,
      string address,
      UserVerify verify,
      UserRole role
      )
    {
      this.userId = Guard.Against.Negative(userId, nameof(userId));
      this.guid = Guard.Against.NullOrEmpty(guid, nameof(guid));
      this.passwordHash = Guard.Against.NullOrEmpty(passwordHash, nameof(passwordHash));
      this.passwordSalt = Guard.Against.NullOrEmpty(passwordSalt, nameof(passwordSalt));
      this.firstname = Guard.Against.NullOrEmpty(firstname, nameof(firstname));
      this.lastname = Guard.Against.NullOrEmpty(lastname, nameof(lastname));
      this.dateOfBirth = Guard.Against.OutOfSQLDateRange(dateOfBirth, nameof(dateOfBirth));
      this.address = Guard.Against.NullOrEmpty(address, nameof(address));

      this.verify = verify;
      this.role = role;
    }
  }
}

