using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrderService.Web.Interfaces;
using StackExchange.Redis.Extensions.Core.Abstractions;
using Swashbuckle.AspNetCore.Annotations;

namespace OrderService.Web.Endpoints.ShipperEndpoints;

public class UpdatePosition : EndpointBaseAsync
  .WithRequest<UpdatePositionRequest>
  .WithActionResult<UpdatePositionResponse>
{

  private readonly ICurrentUserService _currentUserService;
  private readonly IRedisClient _redisClient;

  public UpdatePosition(ICurrentUserService currentUserService, IRedisClient redisClient)
  {
    _currentUserService = currentUserService;
    _redisClient = redisClient;
  }

  [Authorize(Roles = "SHIPPER")]
  [HttpPost(UpdatePositionRequest.Route)]
  [SwaggerOperation(
    Summary = "Update shipper position GPS",
    Description = "Update shipper position GPS",
    OperationId = "Shipper.updatePosition",
    Tags = new[] { "ShipperEndpoints" })
  ]
  public override async Task<ActionResult<UpdatePositionResponse>> HandleAsync(UpdatePositionRequest request, CancellationToken cancellationToken = default)
  {
    var isSuccess = await _redisClient.Db0.AddAsync("Test", "abc");

    Console.WriteLine(isSuccess);

    var response = new UpdatePositionResponse("Ok");

    return Ok(response);
  }
}
