namespace wuav_api.Identity;

public class AppRole
{
    public int Id { get; set; } 
    public string Name { get; set; }
    
    public AppRole() { }
    
    public AppRole(int id, string name)
    {
        Id = id;
        Name = name;
    }
}