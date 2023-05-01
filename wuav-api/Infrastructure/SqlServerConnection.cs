using System.Data;
using System.Data.SqlClient;
namespace wuav_api.Infrastructure;

public class SqlServerConnection
{
    private readonly string _connectionString;

    public SqlServerConnection(IConfiguration configuration)
    {
        var connectionString = DotNetEnv.Env.GetString("SqlConnector");
        _connectionString = connectionString;
    }

    public IDbConnection CreateConnection()
        => new SqlConnection(_connectionString);
}