using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using OrderService.Core.Interfaces;
using OrderService.Web.Interfaces;
using Swashbuckle.AspNetCore.Annotations;

namespace OrderService.Web.Endpoints.AuthenticationEndpoints;

public class AuthenLogin : EndpointBaseAsync
  .WithRequest<AuthenRequest>
  .WithActionResult<AuthenResponse>
{

  private readonly IAuthenticationService _authenticationService;
  private readonly ITokenService _tokenService;
  public AuthenLogin(IAuthenticationService authenticationService, ITokenService tokenService)
  {
    _authenticationService = authenticationService;
    _tokenService = tokenService;
  }


  [HttpPost("/Login")]
  [SwaggerOperation(
    Summary = "Login by email/password",
    Description = "Login by email/password",
    OperationId = "Authen.Login",
    Tags = new[] { "Authen" }
    )
  ]
  public override async Task<ActionResult<AuthenResponse>> HandleAsync(AuthenRequest request, CancellationToken cancellationToken = default)
  {
    var user = await _authenticationService.AuthenticationAsync(request.Email, request.Password);

    if (user.Errors.Any())
    {
      return BadRequest(user.Errors);
    }

    string token = _tokenService.GenerateToken(user);

    var response = new AuthenResponse(token, user.Value.email, user.Value.address, user.Value.phoneNumber, user.Value.role.roleName);

    return Ok(response);
  }
}
