using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrderService.Core.Interfaces;
using OrderService.Core.RabbitMqDto;
using OrderService.Web.Interfaces;
using Swashbuckle.AspNetCore.Annotations;

namespace OrderService.Web.Endpoints.ProductEndpoints;

public class Peek : EndpointBaseAsync
  .WithRequest<PeekProductRequest>
  .WithActionResult
{

  private readonly IProduceProductRequestService _produceProductRequestService;
  private readonly ICurrentUserService _currentUserService;
  public Peek(IProduceProductRequestService produceProductRequestService, ICurrentUserService currentUserService)
  {

    _produceProductRequestService = produceProductRequestService;
    _currentUserService = currentUserService;
  }

  [Authorize]
  [HttpGet(PeekProductRequest.Route)]
  [SwaggerOperation(
    Summary = "Request to fetch product data by urls",
    Description = "Request to fetch product data by urls",
    OperationId = "Product.Request",
    Tags = new[] { "ProductEndpoints" })
  ]
#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
  public override async Task<ActionResult> HandleAsync([FromQuery] PeekProductRequest request, CancellationToken cancellationToken = default)
  {
    //if (request.ProductUrl == null)
    //{
    //  return BadRequest(request.ProductUrl);
    //}

    //var rabbitProductRequest = new RabbitRequestProductData(_currentUserService.TryParseUserId(), request.ProductUrl);
    //_produceProductRequestService.SendToQueue(rabbitProductRequest);

    return Ok("sent to queue, please keep track by connect to the notification hub [/hub]");

  }
}
