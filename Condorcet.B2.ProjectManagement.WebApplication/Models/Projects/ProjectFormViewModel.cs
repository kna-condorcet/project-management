using System.ComponentModel.DataAnnotations;
using Condorcet.B2.ProjectManagement.WebApplication.Models.Projects.Validation;

namespace Condorcet.B2.ProjectManagement.WebApplication.Models.Projects;

public class ProjectFormViewModel : IValidatableObject
{
    public int? Id { get; set; }

    [Required(ErrorMessage = "Le titre est obligatoire")]
    [StringLength(100, ErrorMessage = "Le titre ne peut pas dépasser 100 caractères")]
    [Display(Name = "Titre du projet")]
    public string Title { get; set; }

    [Required(ErrorMessage = "Le code projet est obligatoire")]
    [RegularExpression(@"^[A-Z]{3}\d{3}$", ErrorMessage = "Le code projet doit suivre le format: 3 lettres majuscules suivies de 3 chiffres")]
    [ProjectCodeValidation]
    [Display(Name = "Code du projet")]
    public string ProjectCode { get; set; }

    [StringLength(500)] public string? Description { get; set; }

    [Required(ErrorMessage = "La date de début est obligatoire")]
    [DataType(DataType.Date)]
    [Display(Name = "Date de début")]
    public DateTime StartDate { get; set; }

    [DataType(DataType.Date)]
    [Display(Name = "Date de fin prévue")]
    public DateTime? ExpectedEndDate { get; set; }

    [Required(ErrorMessage = "La priorité est obligatoire")]
    [Display(Name = "Priorité du projet")]
    public ProjectPriority Priority { get; set; }

    [Required]
    [Range(0, double.MaxValue, ErrorMessage = "Le budget doit être positif")]
    [DataType(DataType.Currency)]
    public decimal Budget { get; set; }

    public bool IsEdition => Id.HasValue;

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        // Validation de la date de fin par rapport à la date de début
        if (ExpectedEndDate.HasValue && ExpectedEndDate.Value < StartDate)
        {
            yield return new ValidationResult(
                "La date de fin prévue doit être postérieure à la date de début",
                new[] { nameof(ExpectedEndDate) });
        }

        // Validation du budget en fonction de la priorité
        if (Priority == ProjectPriority.Critical && Budget < 10000)
        {
            yield return new ValidationResult(
                "Les projets de priorité 1 doivent avoir un budget minimum de 10 000 €",
                new[] { nameof(Budget), nameof(Priority) });
        }

        // Validation spéciale pour certains codes projet (exemple)
        if (ProjectCode.StartsWith("AB") && StartDate.Year < 2025)
        {
            yield return new ValidationResult(
                "Les projets de type 'AB' ne peuvent être créés qu'à partir de 2025",
                new[] { nameof(ProjectCode), nameof(StartDate) });
        }
    }
}