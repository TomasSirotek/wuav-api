using System.Runtime.InteropServices.JavaScript;

namespace wuav_api.Identity;

public class AppUser
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password_hash { get; set; } // needs to be like this from the db 
    
    public List<AppRole> Roles { get; set; }

    public AppUser() { }
    
    public AppUser(int id, string name, string email, string Password_hash,List<AppRole> roles)
    {
        Id = id;
        Name = name;
        Email = email;
        this.Password_hash = Password_hash;
        Roles = roles;
    }
}