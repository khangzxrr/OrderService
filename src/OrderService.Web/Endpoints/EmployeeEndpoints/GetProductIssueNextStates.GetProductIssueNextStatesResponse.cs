namespace OrderService.Web.Endpoints.EmployeeEndpoints;

public class GetProductIssueNextStatesResponse
{
  public IEnumerable<string> nextState { get; set; }

  public GetProductIssueNextStatesResponse(IEnumerable<string> nextState)
  {
    this.nextState = nextState;
  }
}
