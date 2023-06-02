using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrderService.Core.Interfaces;
using OrderService.Core.ProductReturnAggregate;
using OrderService.Core.ProductReturnAggregate.specifications;
using OrderService.SharedKernel.Interfaces;
using Swashbuckle.AspNetCore.Annotations;

namespace OrderService.Web.Endpoints.EmployeeEndpoints;

public class GetProductIssueNextStates : EndpointBaseAsync
  .WithRequest<GetProductIssueNextStatesRequest>
  .WithActionResult<GetProductIssueNextStatesResponse>
{

  private readonly IRepository<ProductIssue> _productIssueRepository;
  private readonly IDetermineNextStateProductIssueService _determineNextStateProductIssueService;

  public GetProductIssueNextStates(IRepository<ProductIssue> productIssueRepository, IDetermineNextStateProductIssueService determineNextStateProductIssueService)
  {
    _productIssueRepository = productIssueRepository;
    _determineNextStateProductIssueService = determineNextStateProductIssueService;
  }


  [Authorize(Roles = "EMPLOYEE,MANAGER")]
  [HttpGet(GetProductIssueNextStatesRequest.Route)]
  [SwaggerOperation(
    Summary = "Get product issue next states",
    Description = "Get product issue next states",
    OperationId = "Employee.ProductIssueNextStates",
    Tags = new[] { "EmployeeEndpoints" })
  ]
  public override async Task<ActionResult<GetProductIssueNextStatesResponse>> HandleAsync([FromQuery] GetProductIssueNextStatesRequest request, CancellationToken cancellationToken = default)
  {
    var spec = new ProductIssueByIdSpec(request.productIssueId);

    var productIssue = await _productIssueRepository.FirstOrDefaultAsync(spec);

    if (productIssue == null)
    {
      return BadRequest("product issue is not found");
    }

    var results = _determineNextStateProductIssueService.getNextStatesOf(productIssue);

    if (!results.IsSuccess)
    {
      return BadRequest("failed to determine next state");
    }

    var response = new GetProductIssueNextStatesResponse(results.Value.Select(r => r.Name));

    return response;
  }
}
