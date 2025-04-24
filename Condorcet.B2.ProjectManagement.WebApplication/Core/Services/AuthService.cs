using System.Security.Claims;
using Condorcet.B2.ProjectManagement.WebApplication.Core.Domain;
using Condorcet.B2.ProjectManagement.WebApplication.Core.Repository;
using Condorcet.B2.ProjectManagement.WebApplication.Models;
using Condorcet.B2.ProjectManagement.WebApplication.Models.Auth;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace Condorcet.B2.ProjectManagement.WebApplication.Core.Services;

public class AuthService: IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;
    
    public AuthService(IUserRepository userRepository, IPasswordHasher passwordHasher)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
    }
    
    public async Task<bool> RegisterUserAsync(RegisterUserDto registerDto)
    {
        var existingUser = await _userRepository.GetByUsernameAsync(registerDto.Username);
        if (existingUser != null)
            return false;
        
        var (hash, salt) = _passwordHasher.HashPassword(registerDto.Password);
        
        var user = new User
        {
            Username = registerDto.Username,
            Email = registerDto.Email,
            PasswordHash = hash,
            Salt = salt,
            FirstName = registerDto.FirstName,
            LastName = registerDto.LastName,
            IsActive = true,
            Role = UserRoles.User
        };
        
        var userId = await _userRepository.CreateUserAsync(user);
        
        return true;
    }
    
    public async Task<User?> AuthenticateAsync(string username, string password)
    {
        var user = await _userRepository.GetByUsernameAsync(username);
        if (user == null || !user.IsActive)
            return null;
        
        bool verified = _passwordHasher.VerifyPassword(password, user.PasswordHash, user.Salt);
        if (!verified)
            return null;
        
        return user;
    }
    
    public async Task<ClaimsPrincipal> CreateClaimsPrincipalAsync(User user)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.FirstName),
            new Claim(ClaimTypes.Email, user.Email)
        };
            claims.Add(new Claim(ClaimTypes.Role, user.Role.ToString()));
        
        var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        return new ClaimsPrincipal(identity);
    }

}