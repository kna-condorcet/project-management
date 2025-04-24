using Condorcet.B2.ProjectManagement.WebApplication.Core.Services;
using Condorcet.B2.ProjectManagement.WebApplication.Models;
using Condorcet.B2.ProjectManagement.WebApplication.Models.Auth;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

namespace Condorcet.B2.ProjectManagement.WebApplication.Controllers;

public class AccountController : Controller
{
    private readonly IAuthService _authService;

    public AccountController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Register(RegisterUserDto model)
    {
        if (!ModelState.IsValid)
            return View();

        var success = await _authService.RegisterUserAsync(model);
        if (!success)
        {
            ModelState.AddModelError("", "Le nom d'utilisateur existe déjà");
            return View();
        }

        return RedirectToAction("Login");
    }

    [HttpGet]
    public IActionResult Login(string? returnUrl = null)
    {
        ViewData["ReturnUrl"] = returnUrl;
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginDto model, string? returnUrl = null)
    {
        ViewData["ReturnUrl"] = returnUrl;
        
        if (!ModelState.IsValid)
            return View();

        var user = await _authService.AuthenticateAsync(model.Username, model.Password);
        if (user == null)
        {
            ModelState.AddModelError("", "Nom d'utilisateur ou mot de passe incorrect");
            return View();
        }

        var principal = await _authService.CreateClaimsPrincipalAsync(user);

        await HttpContext.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            principal,
            new AuthenticationProperties
            {
                IsPersistent = model.RememberMe,
                ExpiresUtc = DateTime.UtcNow.AddDays(model.RememberMe ? 14 : 1)
            });
        
        return RedirectToLocal(returnUrl);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction("Index", "Home");
    }
    
    public async Task<IActionResult> AccessDenied(string? returnUrl = null)
    {
        ViewData["ReturnUrl"] = returnUrl;
        return View();
    }
    
    private IActionResult RedirectToLocal(string returnUrl)
    {
        if (Url.IsLocalUrl(returnUrl))
            return Redirect(returnUrl);
        return RedirectToAction("Index", "Home");
    }
}