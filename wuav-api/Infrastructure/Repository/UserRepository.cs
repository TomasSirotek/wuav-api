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
        var sql = @"SELECT Id, Name, Email, Password_Hash FROM app_user";
            
        var users = await cnn.QueryAsync<AppUser>(sql);
        return users.ToList();
    }

    public Task<AppUser> GetUserByIdAsync(string id)
    {
        throw new NotImplementedException();
    }

    public Task<AppUser> GetAsyncByEmailAsync(string email)
    {
        throw new NotImplementedException();
    }
}