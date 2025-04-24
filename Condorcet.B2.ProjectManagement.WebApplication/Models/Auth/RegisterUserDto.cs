using System.ComponentModel.DataAnnotations;

namespace Condorcet.B2.ProjectManagement.WebApplication.Models.Auth;

public class RegisterUserDto : IValidatableObject
{
    [Required, StringLength(50)]
    [Display(Name = "Nom d'utilisateur")]
    public string Username { get; set; }

    [Required, EmailAddress]
    [Display(Name = "Email")]
    public string Email { get; set; }

    [Required, StringLength(100, MinimumLength = 3)]
    [Display(Name = "Mot de passe")]
    public string Password { get; set; }

    [Compare("Password", ErrorMessage = "Les mots de passe ne correspondent pas.")]
    [Display(Name = "Confirmer le mot de passe")]
    public string ConfirmPassword { get; set; }

    [Display(Name = "Prénom")]
    public string FirstName { get; set; }
    
    [Display(Name = "Nom")]
    public string LastName { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        return Enumerable.Empty<ValidationResult>();
    }
}