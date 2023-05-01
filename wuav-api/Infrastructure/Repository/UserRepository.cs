using Dapper;
using wuav_api.Identity;
using wuav_api.Infrastructure.Repository.Interface;

namespace wuav_api.Infrastructure.Repository;

public class UserRepository : IUserRepository
{
    private readonly SqlServerConnection _connection;

    public UserRepository(SqlServerConnection connection)
    {
        _connection = connection;
    }
    public async Task<List<AppUser>> GetAllUsersAsync()
    {
        using var cnn = _connection.CreateConnection();
        var sql = @"SELECT DISTINCT *
                            FROM app_user u
                            INNER JOIN user_role ur ON u.id = ur.user_id
                            INNER JOIN app_role r ON ur.role_id = r.id";
        
        Dictionary<string, AppUser> userRoles = new Dictionary<string, AppUser>();
        IEnumerable<AppUser> users = await cnn.QueryAsync<AppUser, AppRole, AppUser>(sql, (u, r) =>
        { 
            if (!userRoles.TryGetValue(u.Id.ToString(), out var userEntry))
            {
                userEntry = u;
                userEntry.Roles = new List<AppRole>();
                userRoles.Add(u.Id.ToString(), userEntry);
            }
                        
            if (r == null)  userEntry.Roles = new List<AppRole>();
            if (r != null) userEntry.Roles.Add(r);
            
            return userEntry;
        },splitOn:"id");

        List<AppUser> appUsers = users.Distinct().ToList();
        return appUsers.Any() ? appUsers.ToList() : null!;
    }

    public async Task<AppUser> GetUserByIdAsync(string id)
    {
        using var cnn = _connection.CreateConnection();
        var sql = @"SELECT DISTINCT *
                            FROM app_user u
                            INNER JOIN user_role ur ON u.id = ur.user_id
                            INNER JOIN app_role r ON ur.role_id = r.id
                            where u.id = @id
                            ";
        
        Dictionary<string, AppUser> userRoles = new Dictionary<string, AppUser>();
        IEnumerable<AppUser> user = await cnn.QueryAsync<AppUser, AppRole, AppUser>(sql, (u, r) =>
        {
            if (!userRoles.TryGetValue(u.Id.ToString(), out var userEntry))
            {
                userEntry = u;
                userEntry.Roles = new List<AppRole>();
                userRoles.Add(u.Id.ToString(), userEntry);
            }
                
            if (r == null)  userEntry.Roles = new List<AppRole>();
            if (r != null) userEntry.Roles.Add(r);

            return userEntry;
                
        },new {Id = id}, splitOn:"id");
        List<AppUser> appUsers = user.Distinct().ToList();
        return appUsers.Any() ? appUsers.FirstOrDefault()! : null!;
    }

    public async Task<AppUser> GetAsyncByEmailAsync(string email)
    {
        using var cnn = _connection.CreateConnection();
        var sql = @"SELECT DISTINCT *
                            FROM app_user u
                            INNER JOIN user_role ur ON u.id = ur.user_id
                            INNER JOIN app_role r ON ur.role_id = r.id
                            where u.email = @email
                            ";
        
        Dictionary<string, AppUser> userRoles = new Dictionary<string, AppUser>();
        IEnumerable<AppUser> user = await cnn.QueryAsync<AppUser, AppRole,AppUser>(sql, (u, r) =>
        {
            if (!userRoles.TryGetValue(u.Id.ToString(), out var userEntry))
            {
                userEntry = u;
                userEntry.Roles = new List<AppRole>();
                userRoles.Add(u.Id.ToString(), userEntry);
            }
                        
            if (r == null)  userEntry.Roles = new List<AppRole>();
            if (r != null) userEntry.Roles.Add(r);
            return userEntry;
        }, new {Email = email });

        List<AppUser> appUsers = user.Distinct().ToList();
        return appUsers.Any() ? appUsers.FirstOrDefault()! : null!;
    }
}