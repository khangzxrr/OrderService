using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrderService.Core.ProductIssueAggregate.specifications;
using OrderService.Core.ProductReturnAggregate;
using OrderService.SharedKernel.Interfaces;
using OrderService.Web.Endpoints.Records;
using OrderService.Web.Interfaces;
using Swashbuckle.AspNetCore.Annotations;

namespace OrderService.Web.Endpoints.EmployeeEndpoints;

public class GetProductIssues : EndpointBaseAsync
  .WithRequest<GetProductIssuesRequest>
  .WithActionResult<GetProductIssuesResponse>
{

  private readonly IRepository<ProductIssue> _productIssueRepository;

  private readonly ICurrentUserService _currentUserService;

  public GetProductIssues(IRepository<ProductIssue> productIssueRepository, ICurrentUserService currentUserService)
  {
    _productIssueRepository = productIssueRepository;
    _currentUserService = currentUserService;
  }

  [Authorize(Roles = "EMPLOYEE,MANAGER")]
  [HttpGet(GetProductIssuesRequest.Route)]
  [SwaggerOperation(
    Summary = "Get product issues",
    Description = "Get product issues",
    OperationId = "Employee.GetProductIssues",
    Tags = new[] { "EmployeeEndpoints" })
  ]
  public override async Task<ActionResult<GetProductIssuesResponse>> HandleAsync([FromQuery] GetProductIssuesRequest request, CancellationToken cancellationToken = default)
  {
    var totalSpec = new ProductIssuePagingatedFilterByEmployeeIdSpec(request.totalTake, request.totalSkip, _currentUserService.TryParseUserId());

    var spec = new ProductIssuePagingatedFilterByEmployeeIdSpec(request.take, request.skip, _currentUserService.TryParseUserId());

    var totalCount = await _productIssueRepository.CountAsync(totalSpec);
    var productIssues = await _productIssueRepository.ListAsync(spec);

    var productIssueRecords = productIssues.Select(ProductIssueRecord.FromEntity);

    var response = new GetProductIssuesResponse(totalCount, request.pageSize, productIssueRecords);

    return Ok(response);
  }
}
