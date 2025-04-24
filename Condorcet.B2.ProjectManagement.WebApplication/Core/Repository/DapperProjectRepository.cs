using Condorcet.B2.ProjectManagement.WebApplication.Core.Domain;
using Condorcet.B2.ProjectManagement.WebApplication.Core.Infrastructure;
using Dapper;

namespace Condorcet.B2.ProjectManagement.WebApplication.Core.Repository;

public class DapperProjectRepository : IProjectRepository
{
    private readonly IDbConnectionProvider _dbConnectionProvider;

    public DapperProjectRepository(IDbConnectionProvider dbConnectionProvider)
    {
        _dbConnectionProvider = dbConnectionProvider;
    }

    public async Task<List<Project>> GetAll()
    {
        using var connection = await _dbConnectionProvider.CreateConnection();
        var result =
            await connection.QueryAsync<Project>(
                "SELECT id, title, project_code as projectCode, description, start_date as startDate, expected_end_date as expectedEndDate, priority, budget FROM projects ORDER BY id");
        return result.ToList();
    }

    public async Task<List<Project>> GetCriticalProjects()
    {
        using var connection = await _dbConnectionProvider.CreateConnection();
        var result =
            await connection.QueryAsync<Project>(
                """
                SELECT id, title, project_code as projectCode, 
                       description, start_date as startDate, expected_end_date as expectedEndDate, priority, budget 
                FROM projects 
                WHERE priority = 1
                ORDER BY id
                """);
        return result.ToList();
    }

    public async Task<List<Project>> GetActiveProjects()
    {
        using var connection = await _dbConnectionProvider.CreateConnection();
        var result =
            await connection.QueryAsync<Project>(
                """
                SELECT id, title, project_code as projectCode, 
                       description, start_date as startDate, expected_end_date as expectedEndDate, priority, budget 
                FROM projects 
                WHERE start_date <= CURRENT_DATE 
                AND (expected_end_date IS NULL OR expected_end_date >= CURRENT_DATE)
                ORDER BY id
                """);
        return result.ToList();
    }

    public async Task<List<Project>> GetUpcomingProjects()
    {
        using var connection = await _dbConnectionProvider.CreateConnection();
        var result =
            await connection.QueryAsync<Project>(
                """
                SELECT id, title, project_code as projectCode, 
                       description, start_date as startDate, expected_end_date as expectedEndDate, priority, budget 
                FROM projects 
                WHERE start_date > CURRENT_DATE
                ORDER BY id
                """);
        return result.ToList();
    }

    public async Task<Project?> GetById(int id)
    {
        using var connection = await _dbConnectionProvider.CreateConnection();
        return await connection.QueryFirstOrDefaultAsync<Project>(
            "SELECT id, title, project_code as projectCode, description, start_date as startDate, expected_end_date as expectedEndDate, priority, budget FROM projects WHERE id = @id",
            new { id });
    }

    public async Task<int> Insert(Project project)
    {
        using var connection = await _dbConnectionProvider.CreateConnection();
        const string sql = """
                           INSERT INTO projects 
                           (
                               title, 
                               project_code, 
                               description, 
                               start_date, 
                               expected_end_date, 
                               priority, 
                               budget
                           )
                           VALUES 
                           (
                               @Title, 
                               @ProjectCode, 
                               @Description, 
                               @StartDate, 
                               @ExpectedEndDate, 
                               @Priority, 
                               @Budget
                           )
                           RETURNING id
                           """;
        var id = await connection.ExecuteScalarAsync<int>(sql, project);

        return id;
    }

    public async Task<int> Update(int id, Project project)
    {
        project.Id = id;
        using var connection = await _dbConnectionProvider.CreateConnection();
        return await connection.ExecuteAsync("""
                                             UPDATE projects SET title = @title, expected_end_date = @expectedEndDate
                                             WHERE id = @id;
                                             """, project);
    }

    public async Task<bool> ProjectCodeExists(string code)
    {
        using var connection = await _dbConnectionProvider.CreateConnection();
        const string sql = """
                           SELECT COUNT(1) FROM projects 
                           WHERE project_code = @code
                           """;

        var count = await connection.ExecuteScalarAsync<int>(
            sql, new { code });

        return count > 0;
    }

    public async Task<bool> ProjectCodeExists(string code, int ignoredId)
    {
        using var connection = await _dbConnectionProvider.CreateConnection();
        const string sql = """
                           SELECT COUNT(1) FROM projects 
                           WHERE project_code = @code and id <> @ignoredId
                           """;

        var count = await connection.ExecuteScalarAsync<int>(
            sql, new { code, ignoredId });

        return count > 0;
    }

    public async Task Delete(int projectId)
    {
        using var connection = await _dbConnectionProvider.CreateConnection();
        const string sql = """
                           DELETE FROM projects 
                           WHERE id = @projectId
                           """;

        await connection.ExecuteAsync(sql, new { projectId });
    }

    public async Task<int> GetTotalProjectsCountAsync()
    {
        using var connection = await _dbConnectionProvider.CreateConnection();
        const string sql = "SELECT COUNT(*) FROM projects";
        return await connection.ExecuteScalarAsync<int>(sql);
    }

    public async Task<int> GetActiveProjectsCountAsync()
    {
        using var connection = await _dbConnectionProvider.CreateConnection();
        const string sql = @"
            SELECT COUNT(*) 
            FROM projects 
            WHERE start_date <= CURRENT_DATE 
            AND (expected_end_date IS NULL OR expected_end_date >= CURRENT_DATE)";
        return await connection.ExecuteScalarAsync<int>(sql);
    }

    public async Task<int> GetUpcomingProjectsCountAsync()
    {
        using var connection = await _dbConnectionProvider.CreateConnection();
        const string sql = "SELECT COUNT(*) FROM projects WHERE start_date > CURRENT_DATE";
        return await connection.ExecuteScalarAsync<int>(sql);
    }

    public async Task<int> GetCriticalProjectsCountAsync()
    {
        using var connection = await _dbConnectionProvider.CreateConnection();
        const string sql = "SELECT COUNT(*) FROM projects WHERE priority = 1";
        return await connection.ExecuteScalarAsync<int>(sql);
    }

    public async Task<decimal> GetTotalBudgetAsync()
    {
        using var connection = await _dbConnectionProvider.CreateConnection();
        const string sql = "SELECT COALESCE(SUM(budget), 0) FROM projects";
        return await connection.ExecuteScalarAsync<decimal>(sql);
    }

    public async Task<IEnumerable<Project>> GetRecentProjectsAsync(int count)
    {
        using var connection = await _dbConnectionProvider.CreateConnection();
        const string sql = @"
            SELECT 
                id, 
                title, 
                project_code AS projectCode, 
                description, 
                start_date AS startDate, 
                expected_end_date AS expectedEndDate, 
                priority, 
                budget
            FROM projects
            ORDER BY id DESC
            LIMIT @count";

        return await connection.QueryAsync<Project>(sql, new { count });
    }
}