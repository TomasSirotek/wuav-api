using System.Runtime.InteropServices.JavaScript;

namespace wuav_api.Identity;

//public record AppUser(int Id, string Name, string Email, string PasswordHash, DateTime CreatedAt);

public class AppUser
{
   public int id;
    public string name;
    public string email;
    public string password_hash;


    public AppUser()
    {
    }
    public AppUser(int id)
    {
       
        this.id = id;
    }
    
   
}