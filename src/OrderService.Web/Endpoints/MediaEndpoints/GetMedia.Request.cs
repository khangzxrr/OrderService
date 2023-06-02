using System.ComponentModel.DataAnnotations;

namespace OrderService.Web.Endpoints.MediaEndpoints;

public class GetMediaRequest
{
  public const string Route = "/medias";

  [Required]
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
  public string url { get; set; }
}
