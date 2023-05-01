using Dapper;
using wuav_api.Domain.Model;
using wuav_api.Identity;
using wuav_api.Infrastructure.Repository.Interface;

namespace wuav_api.Infrastructure.Repository;

public class ProjectRepository : IProjectRepository
{
    private readonly SqlServerConnection _connection;

    public ProjectRepository(SqlServerConnection connection)
    {
        _connection = connection;
    }

    public Task<Project> GetAllProjectsByUserIdAsync(string userId)
    {
        using var cnn = _connection.CreateConnection();
        
        //
        // var sql = @"SELECT DISTINCT *
        //                     FROM project p
        //                     INNER JOIN user_project up ON p.id = up.user_id
        //                     INNER JOIN user u ON up.role_id = u.id";
        //
        //
        // Dictionary<string, Project> userProjects = new Dictionary<string, Project>();
        // IEnumerable<Project> users = await cnn.QueryAsync<Project, AppUser, Project>(sql, (u, r) =>
        // { 
        //     if (!userRoles.TryGetValue(u.Id.ToString(), out var userEntry))
        //     {
        //         userEntry = u;
        //         userEntry.Roles = new List<AppRole>();
        //         userRoles.Add(u.Id.ToString(), userEntry);
        //     }
        //                 
        //     if (r == null)  userEntry.Roles = new List<AppRole>();
        //     if (r != null) userEntry.Roles.Add(r);
        //     
        //     return userEntry;
        // },splitOn:"id");
        //
        // List<AppUser> appUsers = users.Distinct().ToList();
        // return appUsers.Any() ? appUsers.ToList() : null!;


        return null;
    }
}