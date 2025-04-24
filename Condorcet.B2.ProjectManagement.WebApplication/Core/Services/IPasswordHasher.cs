namespace Condorcet.B2.ProjectManagement.WebApplication.Core.Services;

public interface IPasswordHasher
{
    (string hash, string salt) HashPassword(string password);
    bool VerifyPassword(string password, string hash, string salt);
}