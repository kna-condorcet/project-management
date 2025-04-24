using System.ComponentModel.DataAnnotations;

namespace Condorcet.B2.ProjectManagement.WebApplication.Models.Auth;

public class LoginDto
{
    [Required]
    [Display(Name = "Nom d'utilisateur")]
    public string Username { get; set; }

    [Required] 
    [Display(Name = "Mot de passe")]
    public string Password { get; set; }
    
    [Display(Name = "Se souvenir de moi")]
    public bool RememberMe { get; set; }
}