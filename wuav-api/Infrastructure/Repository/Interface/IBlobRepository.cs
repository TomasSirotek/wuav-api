namespace wuav_api.Infrastructure.Repository.Interface;

public interface IBlobRepository
{
    Task<string> UploadFileAsync(string fileName, string filePath);
}