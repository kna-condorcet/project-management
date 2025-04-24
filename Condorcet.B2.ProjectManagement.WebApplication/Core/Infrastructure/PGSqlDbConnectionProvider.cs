using System.Data;
using Npgsql;

namespace Condorcet.B2.ProjectManagement.WebApplication.Core.Infrastructure;

internal class PGSqlDbConnectionProvider : IDbConnectionProvider
{
    private string _connectionString;

    public PGSqlDbConnectionProvider(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("Default");
    }

    public async Task<IDbConnection> CreateConnection()
    {
        var connection = new NpgsqlConnection(_connectionString);
        await connection.OpenAsync();
        return connection;
    }
}