using OrderService.Core.ContributorAggregate;
using OrderService.SharedKernel.Interfaces;
using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace OrderService.Web.Endpoints.ContributorEndpoints;

public class Create : EndpointBaseAsync
  .WithRequest<CreateContributorRequest>
  .WithActionResult<CreateContributorResponse>
{
  private readonly IRepository<Contributor> _repository;

  public Create(IRepository<Contributor> repository)
  {
    _repository = repository;
  }

  [HttpPost("/Contributors")]
  [SwaggerOperation(
    Summary = "Create a new Contributor",
    Description = "Create a new Contributor",
    OperationId = "Contributor.Create",
    Tags = new[] { "ContributorEndPoints" }
    )
   ]
  public override async Task<ActionResult<CreateContributorResponse>> HandleAsync(
      CreateContributorRequest request,
      CancellationToken cancellationToken = new())
  {
    if (request.Name == null)
    {
      return BadRequest();
    }

    var newContributor = new Contributor(request.Name);
    var createdContributor = await _repository.AddAsync(newContributor, cancellationToken);
    var response = new CreateContributorResponse(
      id: createdContributor.Id,
      name: createdContributor.Name
      );

    return Ok(response);


  }

}
