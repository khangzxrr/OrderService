using Ardalis.ApiEndpoints;
using OrderService.Core.Interfaces;
using OrderService.Web.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace OrderService.Web.Endpoints.AuthenticationEndpoints;

public class AuthenRegister : EndpointBaseAsync
  .WithRequest<RegisterRequest>
  .WithActionResult<RegisterResponse>
{

  private readonly IAuthenticationService _authenticationService;
  private readonly ITokenService _tokenService;

  public AuthenRegister(IAuthenticationService authenticationService, ITokenService tokenService)
  {
     _authenticationService = authenticationService;
    _tokenService = tokenService;
  }


  [HttpPost(RegisterRequest.Route)]
  [SwaggerOperation(
    Summary = "Register and get token",
    Description = "Register and get token",
    OperationId = "Authen.Register",
    Tags = new[] { "Authen" }
    )
  ]

  public override async Task<ActionResult<RegisterResponse>> HandleAsync(
    RegisterRequest request, CancellationToken cancellationToken = default)
  {
    var userResult = await _authenticationService.CreateNewUserAsync(request.Email, request.PhoneNumber, request.Password, request.FirstName, request.LastName, request.DateOfBirth!.Value, request.Address);

    if (userResult == null)
    {
      return BadRequest("server error");
    }

    if (!userResult.IsSuccess)
    {
      return BadRequest(userResult.Errors);
    }

    var token = _tokenService.GenerateToken(userResult.Value);

    var response = new RegisterResponse(token, userResult.Value.email, userResult.Value.address, userResult.Value.phoneNumber, userResult.Value.role.roleName);

    return Ok(response);
  }
}
