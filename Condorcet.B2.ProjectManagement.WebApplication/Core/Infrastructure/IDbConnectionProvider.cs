using System.Data;

namespace Condorcet.B2.ProjectManagement.WebApplication.Core.Infrastructure;

public interface IDbConnectionProvider
{
    public Task<IDbConnection> CreateConnection();
}