using wuav_api.Domain.Model;
using wuav_api.Infrastructure.Repository.Interface;
using wuav_api.Services.Interface;

namespace wuav_api.Services;

public class ProjectService : IProjectService
{
    
    private readonly IProjectRepository _projectRepository;

    public ProjectService(IProjectRepository projectRepository)
    {
        _projectRepository = projectRepository;
    }

    public Task<Project> GetAllProjectsByUserIdAsync(string userId)
    {
        return _projectRepository.GetAllProjectsByUserIdAsync(userId);
    }
}