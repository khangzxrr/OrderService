using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using OrderService.Core.UserAggregate;
using OrderService.Core.UserAggregate.Specifications;
using OrderService.SharedKernel.Interfaces;
using OrderService.Web.Endpoints.Records;
using Swashbuckle.AspNetCore.Annotations;

namespace OrderService.Web.Endpoints.AdminEndpoints;

public class GetUsers : EndpointBaseAsync
  .WithRequest<GetUsersRequest>
  .WithActionResult<GetUsersResponse>
{

  private readonly IRepository<User> _userRepository;

  public GetUsers(IRepository<User> userRepository)
  {
    _userRepository = userRepository;
  }

  [HttpGet(GetUsersRequest.Route)]
  //[Authorize(Roles = "ADMIN")]
  [SwaggerOperation(
    Summary = "Get users",
    Description = "Get users",
    OperationId = "admin.getUsers",
    Tags = new[] { "AdminEndpoints" })
  ]
  public override async Task<ActionResult<GetUsersResponse>> HandleAsync([FromQuery] GetUsersRequest request, CancellationToken cancellationToken = default)
  {
    var countSpec = new UserPaginatedSpec(request.totalSkip, request.totalTake);

    var listSpec = new UserPaginatedSpec(request.skip, request.take);

    var total = await _userRepository.CountAsync(countSpec);
    var users = await _userRepository.ListAsync(listSpec);

    var userRecords = users.Select(UserRecord.FromEntity);

    var response = new GetUsersResponse(total, request.pageSize, userRecords);

    return Ok(response);
  }
}
