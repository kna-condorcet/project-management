using System.Data;
using Npgsql;

namespace Condorcet.B2.ProjectManagement.WebApplication.Core.Infrastructure;

internal class PGSqlDbConnectionProvider : IDbConnectionProvider
{
    public async Task<IDbConnection> CreateConnection()
    {
        var connection = new NpgsqlConnection("Host=localhost;Database=project_management;Username=condorcet;Password=condorcet");
        await connection.OpenAsync();
        return connection;
    }
}