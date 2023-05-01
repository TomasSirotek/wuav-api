using wuav_api.Identity;

namespace wuav_api.Services.Interface;

public interface IUserService
{
    Task<List<AppUser>> GetAllUsersAsync();
        
    Task<AppUser> GetUserByIdAsync(string id);

    Task<AppUser> GetAsyncByEmailAsync(string email);
} 