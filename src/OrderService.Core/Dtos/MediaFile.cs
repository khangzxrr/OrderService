namespace OrderService.Core.Dtos;
public record MediaFile(string fileName, Stream fileStream, long fileSize)
{
}
