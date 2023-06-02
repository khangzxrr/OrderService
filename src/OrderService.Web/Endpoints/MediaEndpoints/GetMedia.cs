using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using Minio.DataModel.Tags;
using OrderService.Core.Interfaces;
using Swashbuckle.AspNetCore.Annotations;

namespace OrderService.Web.Endpoints.MediaEndpoints;

public class GetMedia : EndpointBaseAsync
  .WithRequest<GetMediaRequest>
  .WithActionResult
{

  private readonly IMediaService _mediaService;

  public GetMedia(IMediaService mediaService)
  {
    _mediaService = mediaService;
  }

  [HttpGet(GetMediaRequest.Route)]
  [SwaggerOperation(
    Summary = "get media",
  Description = "get media",
    OperationId = "Media.upload",
    Tags = new[] { "MediaEndpoints" })
  ]
  public override async Task<ActionResult> HandleAsync([FromQuery] GetMediaRequest request, CancellationToken cancellationToken = default)
  {
    var stream = await _mediaService.getFile(request.url);

    return new FileContentResult(stream.ToArray(), "image/jpeg");
  }
}
