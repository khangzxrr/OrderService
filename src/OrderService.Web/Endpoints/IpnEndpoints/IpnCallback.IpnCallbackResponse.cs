namespace OrderService.Web.Endpoints.IpnEndpoints;

public class IpnCallbackResponse
{
  public string RspCode { get; set; }
  public string Message { get; set; }

  public IpnCallbackResponse(string code, string message)
  {
    RspCode = code;
    Message = message;
  }
}
