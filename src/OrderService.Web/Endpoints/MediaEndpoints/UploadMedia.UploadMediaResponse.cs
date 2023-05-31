namespace OrderService.Web.Endpoints.MediaEndpoints;

public class UploadMediaResponse
{
  public string fileName { get; set; }

  public UploadMediaResponse(string fileName)
  {
    this.fileName = fileName;
  }
}
