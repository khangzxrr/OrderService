using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Minio.DataModel.Tags;
using OrderService.Core.Interfaces;
using OrderService.Core.ShipperAggregate;
using OrderService.Core.UserAggregate;
using OrderService.Core.UserAggregate.Specifications;
using OrderService.SharedKernel.Interfaces;
using OrderService.Web.Endpoints.Records;
using Swashbuckle.AspNetCore.Annotations;

namespace OrderService.Web.Endpoints.AdminEndpoints;

public class CreateUser : EndpointBaseAsync
  .WithRequest<CreateUserRequest>
  .WithActionResult<CreateUserResponse>
{

  private readonly IRepository<User> _userRepository;
  private readonly IAuthenticationService _authenticationService;
  private readonly IRepository<Role> _roleRepository;

  private readonly IRepository<Shipper> _shipperRepository;

  public CreateUser(IRepository<User> userRepository, IAuthenticationService authenticationService, IRepository<Role> roleRepository, IRepository<Shipper> shipperRepository)
  {
    _userRepository = userRepository;
    _authenticationService = authenticationService;
    _roleRepository = roleRepository;
    _shipperRepository = shipperRepository;
  }


  [HttpPut(CreateUserRequest.Route)]
  [Authorize(Roles = "ADMIN")]
  [SwaggerOperation(
    Summary = "Create new user",
    Description = "Create new user",
    OperationId = "admin.createUser",
    Tags = new[] { "AdminEndpoints" })
  ]
  public override async Task<ActionResult<CreateUserResponse>> HandleAsync(CreateUserRequest request, CancellationToken cancellationToken = default)
  {

    var result = await _authenticationService.CreateNewUserAsync(request.email, request.phoneNumber, request.password, request.fullname, request.address);

    if (result.Errors.Any())
    {
      return BadRequest(result.Errors);
    }

    var user = result.Value;

    RoleEnum roleEnum = RoleEnum.CUSTOMER;

    if (request.role == "EMPLOYEE")
    {
      roleEnum = RoleEnum.EMPLOYEE;
    }
    else if (request.role == "SHIPPER")
    {
      roleEnum = RoleEnum.SHIPPER;
    } 
    else if (request.role == "MANAGER")
    {
      roleEnum = RoleEnum.MANAGER;
    }

    var roleSpec = new RoleByNameSpec(roleEnum);
    var role = await _roleRepository.FirstOrDefaultAsync(roleSpec);

    user.setRole(role!);

    
    if (roleEnum == RoleEnum.SHIPPER)
    {

      user.setShipper(new Shipper());
    }

    await _userRepository.SaveChangesAsync();

    var response = new CreateUserResponse(UserRecord.FromEntity(user));

    return Ok(response);
  }
}
