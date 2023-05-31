using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace OrderService.Web.Endpoints.MediaEndpoints;

public class UploadMediaRequest
{
  public const string Route = "/medias/upload";

  [FromForm]
  [Required]
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
  public IFormFile file { get; set; }



}
