using System.Security.Claims;
using Condorcet.B2.ProjectManagement.WebApplication.Core.Domain;
using Condorcet.B2.ProjectManagement.WebApplication.Models;
using Condorcet.B2.ProjectManagement.WebApplication.Models.Auth;

namespace Condorcet.B2.ProjectManagement.WebApplication.Core.Services;

public interface IAuthService
{
    Task<bool> RegisterUserAsync(RegisterUserDto registerDto);
    Task<User?> AuthenticateAsync(string username, string password);
    Task<ClaimsPrincipal> CreateClaimsPrincipalAsync(User user);
}