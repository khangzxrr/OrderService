using Ardalis.Result;
using OrderService.Core.Interfaces;
using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace OrderService.Web.Endpoints.ContributorEndpoints;

public class Delete : EndpointBaseAsync
  .WithRequest<DeleteContributorRequest>
  .WithoutResult
{

  private readonly IDeleteContributorService _deleteContributorService;

  public Delete(IDeleteContributorService service)
  {
    _deleteContributorService = service;
  }

  [HttpDelete(DeleteContributorRequest.Route)]
  [SwaggerOperation(
      Summary = "Deletes a contributor",
      Description = "Deletes a contributor",
      OperationId = "Contributors.Delete",
      Tags = new[] { "ContributorEndpoints" })
    ]
  public override async Task<ActionResult> HandleAsync(
    DeleteContributorRequest request,
    CancellationToken cancellationToken)
  {
    var result = await _deleteContributorService.DeleteContributor(request.ContributorId);

    if (result.Status == ResultStatus.NotFound)
    {
      return NotFound();
    }

    return NoContent();
  }
}
