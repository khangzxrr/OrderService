using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrderService.Core.UserAggregate;
using OrderService.Core.UserAggregate.Specifications;
using OrderService.SharedKernel.Interfaces;
using OrderService.Web.Endpoints.Records;
using Swashbuckle.AspNetCore.Annotations;

namespace OrderService.Web.Endpoints.ManagerEndpoints;

public class GetCustomers : EndpointBaseAsync
  .WithRequest<GetCustomersRequest>
  .WithActionResult<GetCustomersResponse>
{

  private readonly IRepository<User> _userRepository; 

  public GetCustomers(IRepository<User> userRepository)
  {
    _userRepository = userRepository;
  }

  [HttpGet(GetCustomersRequest.Route)]
  [SwaggerOperation(
    Summary = "Get customers list",
    Description = "Get customers list",
    OperationId = "Customer.all",
    Tags = new[] { "ManagerEndpoints" })
  ]
  [Authorize(Roles = "MANAGER")]
  public override async Task<ActionResult<GetCustomersResponse>> HandleAsync([FromQuery] GetCustomersRequest request, CancellationToken cancellationToken = default)
  {
    var spec = new UserPaginatedSpec(request.skip, request.take, "CUSTOMER");
    var totalSpec = new UserPaginatedSpec(request.totalSkip, request.totalTake, "CUSTOMER");

    var totalCount = await _userRepository.CountAsync(totalSpec);
    var users = await _userRepository.ListAsync(spec);

    var customerRecords = users.Select(CustomerRecord.FromEntity);

    var response = new GetCustomersResponse(totalCount, request.pageSize, customerRecords);

    return Ok(response);
  }
}
