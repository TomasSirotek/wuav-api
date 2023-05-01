using System.Runtime.InteropServices.JavaScript;
using wuav_api.Domain.Model;

namespace wuav_api.Identity;

public class AppUser
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password_hash { get; set; } // needs to be like this from the db 
    
    public List<AppRole> Roles { get; set; }
    
    public List<Project> Projects { get; set; }

    public AppUser() { }
    
    public AppUser(int id, string name, string email, string Password_hash,List<AppRole> roles,List<Project> projects)
    {
        Id = id;
        Name = name;
        Email = email;
        this.Password_hash = Password_hash;
        Roles = roles;
        Projects = projects;
    }
}