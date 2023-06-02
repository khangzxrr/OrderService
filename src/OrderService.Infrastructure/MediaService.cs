
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

  public async Task<MemoryStream> getFile(string fileName)
  {
    MemoryStream imageStream = new MemoryStream();

    var existObjectArgs = new GetObjectArgs()
        .WithBucket(bucketName)
        .WithObject(fileName)
        .WithCallbackStream(async (stream, cancellationToken) =>
        {
          await stream.CopyToAsync(imageStream).ConfigureAwait(false);
          
          stream.Dispose();
        });


    var obj = await _minioClient.GetObjectAsync(existObjectArgs);

    return imageStream;
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

  public async Task<string> downloadImage(string imageUrl)
  {
    await makeSureBucketExist();

    using (HttpClient webClient = new HttpClient())
    {
      var response = await webClient.GetAsync(imageUrl); 

      response.EnsureSuccessStatusCode();

      var contentType = response.Content.Headers.ContentType!.ToString();

      var stream = response.Content.ReadAsStream();

      var newFileName = generateObjectname("jpg");

      long length = long.Parse(response.Content.Headers.First(h => h.Key.Equals("Content-Length")).Value.First());

      var mediaFile = new MediaFile(newFileName, stream, length);

      var newUpload = await uploadFile(mediaFile);

      return newUpload;
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
