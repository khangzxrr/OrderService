using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using OrderService.Core.ProductReturnAggregate;
using OrderService.Core.ProductReturnAggregate.specifications;
using OrderService.SharedKernel.Interfaces;
using OrderService.Web.Endpoints.Records;
using Swashbuckle.AspNetCore.Annotations;

namespace OrderService.Web.Endpoints.EmployeeEndpoints;

public class UpdateProductIssue : EndpointBaseAsync
  .WithRequest<UpdateProductIssueRequest>
  .WithActionResult<UpdateProductIssueResponse>
{

  private readonly IRepository<ProductIssue> _productIssueRepository; 

  public UpdateProductIssue(IRepository<ProductIssue> productIssueRepository)
  {
    _productIssueRepository = productIssueRepository;
  }

  [Authorize(Roles = "EMPLOYEE,MANAGER")]
  [HttpPatch(UpdateProductIssueRequest.Route)]
  [SwaggerOperation(
    Summary = "Update product issues",
    Description = "Update product issues",
    OperationId = "Employee.UpdateProductIssues",
    Tags = new[] { "EmployeeEndpoints" })
  ]
  public override async Task<ActionResult<UpdateProductIssueResponse>> HandleAsync([FromBody] UpdateProductIssueRequest request, CancellationToken cancellationToken = default)
  {
    if (request.finishStatus == null && request.status == null)
    {
      return BadRequest("at least one field is not empty");
    }



    var spec = new ProductIssueByIdSpec(request.productIssueId);

    var productIssue = await _productIssueRepository.FirstOrDefaultAsync(spec);

    if (productIssue == null)
    {
      return BadRequest("product issue is not found");
    }


    

    if (!request.finishStatus.IsNullOrEmpty())
    {

      ProductIssueFinishStatus productIssueFinishStatus;
      bool isSuccessParsed = ProductIssueFinishStatus.TryFromName(request.finishStatus!, out productIssueFinishStatus);

      if (!isSuccessParsed)
      {
        return BadRequest("failed to parse product issue finish status");
      }

      productIssue.SetFinishStatus(productIssueFinishStatus); 
    }

    if (!request.status.IsNullOrEmpty())
    {
      ProductIssueStatus productIssueStatus;
      bool isSuccessParsed = ProductIssueStatus.TryFromName(request.status!, out productIssueStatus);

      if (!isSuccessParsed)
      {
        return BadRequest("failed to parse product issue status");
      }

      productIssue.SetStatus(productIssueStatus);
    }

    await _productIssueRepository.SaveChangesAsync();

    var response = new UpdateProductIssueResponse(ProductIssueRecord.FromEntity(productIssue));

    return Ok(response);
  }
}
