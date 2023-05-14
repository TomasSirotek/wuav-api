using Azure.Storage.Blobs;
using wuav_api.Infrastructure.Repository.Interface;

namespace wuav_api.Infrastructure.Repository;

public class BlobRepository : IBlobRepository
{
    private readonly BlobServiceClient _blobServiceClient;
    private BlobContainerClient _client;
    
    public BlobRepository(BlobServiceClient blobServiceClient)
    {
        _blobServiceClient = blobServiceClient;
        _client = _blobServiceClient.GetBlobContainerClient("container");
    }
    
    public async Task<string> UploadFileAsync(string fileName, string filePath)
    {
        var blobClient = _client.GetBlobClient(fileName);
        await using var stream = File.OpenRead(filePath);
        await blobClient.UploadAsync(stream);
        return blobClient.Uri.AbsoluteUri;
    }
}