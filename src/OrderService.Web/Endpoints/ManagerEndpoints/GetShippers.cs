using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrderService.Core.ShipperAggregate;
using OrderService.Core.ShipperAggregate.specifications;
using OrderService.SharedKernel.Interfaces;
using OrderService.Web.Endpoints.Records;
using Swashbuckle.AspNetCore.Annotations;

namespace OrderService.Web.Endpoints.ManagerEndpoints;

public class GetShippers : EndpointBaseAsync
  .WithRequest<GetShippersRequest>
  .WithActionResult<GetShippersResponse>
{

  private readonly IRepository<Shipper> _shipperRepository;

  public GetShippers(IRepository<Shipper> shipperRepository)
  {
    _shipperRepository = shipperRepository;
  }

  [HttpGet(GetShippersRequest.Route)]
  [SwaggerOperation(
   Summary = "Get shippers list",
   Description = "Get shippers list",
   OperationId = "Shipper.getAll",
   Tags = new[] { "ManagerEndpoints" })
 ]
  [Authorize(Roles = "MANAGER")]
  public override async Task<ActionResult<GetShippersResponse>> HandleAsync([FromQuery] GetShippersRequest request, CancellationToken cancellationToken = default)
  {

    var spec = new ShipperPaginatedSpec(request.skip, request.take);

    var totalCount = await _shipperRepository.CountAsync();
    var shippers = await _shipperRepository.ListAsync(spec);

    var shipperRecords = shippers.Select(ShipperRecord.FromEntity);

    var response = new GetShippersResponse(totalCount, request.pageSize, shipperRecords);

    return Ok(response);
  }
}
