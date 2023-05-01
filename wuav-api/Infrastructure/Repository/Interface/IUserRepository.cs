using wuav_api.Identity;

namespace wuav_api.Infrastructure.Repository.Interface;

public interface IUserRepository
{
    Task<List<AppUser>> GetAllUsersAsync();

    Task<AppUser> GetUserByIdAsync(string id);

    Task<AppUser> GetAsyncByEmailAsync(string email);
}