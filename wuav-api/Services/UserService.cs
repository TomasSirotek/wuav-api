using wuav_api.Identity;
using wuav_api.Infrastructure.Repository.Interface;
using wuav_api.Services.Interface;

namespace wuav_api.Services;

public class UserService : IUserService
{
    
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    
    public Task<List<AppUser>> GetAllUsersAsync()
    {
        return _userRepository.GetAllUsersAsync();
    }

    public Task<AppUser> GetUserByIdAsync(string id)
    {
        return _userRepository.GetUserByIdAsync(id);
    }

    public Task<AppUser> GetAsyncByEmailAsync(string email)
    {
        return _userRepository.GetAsyncByEmailAsync(email);
    }
}