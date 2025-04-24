using Condorcet.B2.ProjectManagement.WebApplication.Core.Domain;

namespace Condorcet.B2.ProjectManagement.WebApplication.Core.Repository;

public interface IProjectRepository
{

    public Task<List<Project>> GetAll();
    public Task<Project?> GetById(int id);
    public Task<int> Insert(Project project);
    public Task<int> Update(int id, Project project);

    Task<bool> ProjectCodeExists(string projectCode);
    Task<bool> ProjectCodeExists(string projectCode, int ignoredId);
    Task Delete(int projectId);
    Task<int> GetTotalProjectsCountAsync();
    Task<int> GetActiveProjectsCountAsync();
    Task<int> GetUpcomingProjectsCountAsync();
    Task<int> GetCriticalProjectsCountAsync();
    Task<decimal> GetTotalBudgetAsync();
    Task<IEnumerable<Project>> GetRecentProjectsAsync(int count);
    Task<List<Project>> GetCriticalProjects();
    Task<List<Project>> GetActiveProjects();
    Task<List<Project>> GetUpcomingProjects();
}