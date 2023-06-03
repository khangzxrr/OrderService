using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Minio.DataModel.Tags;
using OrderService.Core.Interfaces;
using OrderService.Web.Interfaces;
using Swashbuckle.AspNetCore.Annotations;

namespace OrderService.Web.Endpoints.AuthenticationEndpoints;

public class UpdateProfile : EndpointBaseAsync
  .WithRequest<UpdateProfileRequest>
  .WithActionResult<UpdateProfileResponse>
{

  private readonly IAuthenticationService _authenticationService;

  private readonly ICurrentUserService _currentUserService;
  public UpdateProfile(IAuthenticationService authenticationService, ICurrentUserService currentUserService)
  {
    _authenticationService = authenticationService;
    _currentUserService = currentUserService;
  }

  [HttpPut(UpdateProfileRequest.Route)]
  [Authorize(Roles = "CUSTOMER")]
  [SwaggerOperation(
    Summary = "Update customer profile",
    Description = "Update customer profile",
    OperationId = "Authen.UpdateProfile",
    Tags = new[] { "Authen" }
    )
  ]
  public override async Task<ActionResult<UpdateProfileResponse>> HandleAsync([FromBody] UpdateProfileRequest request, CancellationToken cancellationToken = default)
  {
    var result = await _authenticationService.UpdateUser(_currentUserService.TryParseUserId(), request.phoneNumber, request.fullName, request.address, request.password);

    if (result.Errors.Any())
    {
      return BadRequest(result.Errors);
    }

    var response = new UpdateProfileResponse(result.Value.fullName, result.Value.address, result.Value.phoneNumber);

    return Ok(response);
  }
}
