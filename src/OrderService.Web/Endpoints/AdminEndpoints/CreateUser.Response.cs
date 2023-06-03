using OrderService.Web.Endpoints.Records;

namespace OrderService.Web.Endpoints.AdminEndpoints;

public class CreateUserResponse
{
  public UserRecord userRecord { get; set; }

  public CreateUserResponse(UserRecord userRecord)
  {
    this.userRecord = userRecord;
  }
}
