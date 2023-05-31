using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using OrderService.Core.Dtos;
using OrderService.Core.Interfaces;
using Swashbuckle.AspNetCore.Annotations;

namespace OrderService.Web.Endpoints.MediaEndpoints;

public class UploadMedia : EndpointBaseAsync
  .WithRequest<UploadMediaRequest>
  .WithActionResult<UploadMediaResponse>
{

  private readonly IMediaService _mediaService;

  public UploadMedia(IMediaService mediaService)
  {
    _mediaService = mediaService;
  }

  [HttpPost(UploadMediaRequest.Route)]
  [SwaggerOperation(
    Summary = "Upload media",
    Description = "Upload media",
    OperationId = "Media.upload",
    Tags = new[] { "MediaEndpoints" })
  ]
  public override async Task<ActionResult<UploadMediaResponse>> HandleAsync([FromForm] UploadMediaRequest request, CancellationToken cancellationToken = default)
  {
    var mediaFile = new MediaFile(request.file.FileName, request.file.OpenReadStream(), request.file.Length);

    var newFileName = await _mediaService.uploadFile(mediaFile);

    var response = new UploadMediaResponse(newFileName);

    return Ok(response);
  }
}
