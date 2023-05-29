using System.Diagnostics;
using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrderService.Core.UserAggregate;
using OrderService.Core.UserAggregate.Specifications;
using OrderService.SharedKernel.Interfaces;
using Swashbuckle.AspNetCore.Annotations;

namespace OrderService.Web.Endpoints.AdminEndpoints;

public class Dashboard : EndpointBaseAsync
  .WithoutRequest
  .WithActionResult<DashboardResponse>
{

  private readonly IRepository<User> _userRepository;

  public Dashboard(IRepository<User> userRepository)
  {
    _userRepository = userRepository;
  }

  [HttpGet(DashboardRequest.Route)]
  [Authorize(Roles = "ADMIN")]
  [SwaggerOperation(
    Summary = "Get dashboard info",
    Description = "Get dashboard info",
    OperationId = "admin.dashboard",
    Tags = new[] { "AdminEndpoints" })
  ]
  public override async Task<ActionResult<DashboardResponse>> HandleAsync(CancellationToken cancellationToken = default)
  {

    var userCountSpec = new UserPaginatedSpec(0, int.MaxValue);
    var totalCount = await _userRepository.CountAsync(userCountSpec);

    var response = new DashboardResponse(totalCount);

    return Ok(response);
  }
}
