
using Azure.Core;
using Minio;
using Minio.Exceptions;
using OrderService.Core.Dtos;
using OrderService.Core.Interfaces;

namespace OrderService.Infrastructure;
public class MediaService : IMediaService
{

  private readonly MinioClient _minioClient;
  private const string bucketName = "upload";


  public MediaService(MinioClient minioClient)
  {
    _minioClient = minioClient;
  }

  public Task<Stream> getFile(string fileName)
  {
    throw new NotImplementedException();
  }

  private string generateObjectname(string fileExtension)
  {
    return $"{Guid.NewGuid()}.{fileExtension}";
  }

  private async Task<bool> isObjectExist(string objectname)
  {

    var existObjectArgs = new GetObjectArgs()
        .WithBucket(bucketName)
        .WithObject(objectname);

    bool isExist = false;

    try
    {
      await _minioClient.GetObjectAsync(existObjectArgs);
      isExist = true;
    }
    catch (ObjectNotFoundException)
    {
      Console.WriteLine($"{objectname} is not found");
    }

    return isExist;
  }

  private async Task makeSureBucketExist()
  {
    var existBucketArgs = new BucketExistsArgs()
        .WithBucket(bucketName);

    bool foundBucket = await _minioClient.BucketExistsAsync(existBucketArgs);
    if (!foundBucket)
    {
      var makeBucketArgs = new MakeBucketArgs()
         .WithBucket(bucketName);

      await _minioClient.MakeBucketAsync(makeBucketArgs);
    }

  }

  public async Task<string[]> uploadFiles(MediaFile[] files)
  {
    await makeSureBucketExist();

    var uploadedFileNames = new List<string>();
    

    foreach(var file in files)
    {

      string newFileName;

      //make sure file name is not exist before create
      
      do
      {
        string extension = file.fileName.Split('.').Last();
        newFileName = generateObjectname(extension);
      } while (await isObjectExist(newFileName));

      var putObjectArgs = new PutObjectArgs()
        .WithBucket(bucketName)
        .WithObject(newFileName)
        .WithObjectSize(file.fileSize)
        .WithStreamData(file.fileStream);

      await _minioClient.PutObjectAsync(putObjectArgs);

      uploadedFileNames.Add(newFileName);

    }

    return uploadedFileNames.ToArray();
  }

  public async Task<string> uploadFile(MediaFile file)
  {
    await makeSureBucketExist();

    string newFileName;

    do
    {
      string extension = file.fileName.Split('.').Last();
      newFileName = generateObjectname(extension);
    } while (await isObjectExist(newFileName));

    var putObjectArgs = new PutObjectArgs()
        .WithBucket(bucketName)
        .WithObject(newFileName)
        .WithObjectSize(file.fileSize)
        .WithStreamData(file.fileStream);

    await _minioClient.PutObjectAsync(putObjectArgs);

    return newFileName;
  }
}
