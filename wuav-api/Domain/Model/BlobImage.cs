namespace wuav_api.Domain.Model;

public class BlobImage
{
    public int Id { get; set; }
    public string ImageType { get; set; }
    public string ImageUrl { get; set; }
    
    
    public BlobImage()
    {
    }
    
    public BlobImage(int id, string imageType, string imageUrl)
    {
        Id = id;
        ImageType = imageType;
        ImageUrl = imageUrl;
    }
}