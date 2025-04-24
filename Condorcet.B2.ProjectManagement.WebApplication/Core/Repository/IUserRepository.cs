using Condorcet.B2.ProjectManagement.WebApplication.Core.Domain;

namespace Condorcet.B2.ProjectManagement.WebApplication.Core.Repository;

public interface IUserRepository
{
    Task<User?> GetByUsernameAsync(string username);
    Task<User?> GetByIdAsync(int id);
    Task<int> CreateUserAsync(User user);
}