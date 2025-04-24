using Condorcet.B2.ProjectManagement.WebApplication.Core.Domain;
using Condorcet.B2.ProjectManagement.WebApplication.Core.Infrastructure;
using Dapper;

namespace Condorcet.B2.ProjectManagement.WebApplication.Core.Repository;

public class DapperUserRepository : IUserRepository
{
    private readonly IDbConnectionProvider _dbConnectionProvider;

    public DapperUserRepository(IDbConnectionProvider dbConnectionProvider)
    {
        _dbConnectionProvider = dbConnectionProvider;
    }
    public async Task<User?> GetByUsernameAsync(string username)
    {
        using var connection = await _dbConnectionProvider.CreateConnection();
        const string sql = "SELECT * FROM users WHERE username = @username";
        return await connection.QuerySingleOrDefaultAsync<User>(sql, new { username });
    }
    
    public async Task<User?> GetByIdAsync(int id)
    {
        using var connection = await _dbConnectionProvider.CreateConnection();
        const string sql = "SELECT * FROM users WHERE id = @Id";
        return await connection.QuerySingleOrDefaultAsync<User>(sql, new { Id = id });
    }
    
    public async Task<int> CreateUserAsync(User user)
    {
        using var connection = await _dbConnectionProvider.CreateConnection();
        const string sql = """
                           INSERT INTO users (username, email, password_hash, salt, first_name, last_name, is_active, role)
                           VALUES (@Username, @Email, @PasswordHash, @Salt, @FirstName, @LastName, @IsActive, @Role)
                           RETURNING id
                           """;
        
        return await connection.ExecuteScalarAsync<int>(sql, user);
    }
}