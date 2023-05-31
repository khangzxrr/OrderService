
using OrderService.Core.Dtos;

namespace OrderService.Core.Interfaces;
public interface IMediaService
{
  Task<string[]> uploadFiles(MediaFile[] files);
  Task<string> uploadFile(MediaFile file);
  Task<Stream> getFile(string fileName);
}
