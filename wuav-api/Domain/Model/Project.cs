namespace wuav_api.Domain.Model;

public class Project
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime Created_at { get; set; }
    public string Status { get; set; }
    
    public List<BlobImage> Images { get; set; }
    
    public Project()
    {
    }
    
    public Project(int id, string name, string description, DateTime created_at, string status)
    {
        Id = id;
        Name = name;
        Description = description;
        Created_at = created_at;
        Status = status;
    }
}