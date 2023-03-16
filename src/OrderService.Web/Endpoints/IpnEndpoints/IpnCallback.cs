using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace OrderService.Web.Endpoints.IpnEndpoints;

public class IpnCallback : EndpointBaseAsync
  .WithRequest<IpnCallbackRequest>
  .WithActionResult
{

  [HttpGet(IpnCallbackRequest.Route)]
  [SwaggerOperation(
    Summary = "Ipn callback Endpoint",
    Description = "Ipn callback Endpoint",
    OperationId = "Ipn.Callback",
    Tags = new[] { "IpnEndpoints" })
  ]
#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
  public override async Task<ActionResult> HandleAsync([FromQuery] IpnCallbackRequest request, CancellationToken cancellationToken = default)
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
  {
    return Ok();
  }
}
