
using OrderService.Core.Dtos;

namespace OrderService.Core.Interfaces;
public interface IMediaService
{
  Task<string[]> uploadFiles(MediaFile[] files);
  Task<string> uploadFile(MediaFile file);
  Task<MemoryStream> getFile(string fileName);

  Task<string> downloadImage(string imageUrl);
}
