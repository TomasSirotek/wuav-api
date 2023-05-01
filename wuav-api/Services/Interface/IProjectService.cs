using wuav_api.Domain.Model;

namespace wuav_api.Services.Interface;

public interface IProjectService
{
    Task<Project> GetAllProjectsByUserIdAsync(string userId);
}