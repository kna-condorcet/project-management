using System.ComponentModel.DataAnnotations;
using Condorcet.B2.ProjectManagement.WebApplication.Core.Repository;

namespace Condorcet.B2.ProjectManagement.WebApplication.Models.Projects.Validation;

[AttributeUsage(AttributeTargets.Property)]
public class ProjectCodeValidationAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
    {
        if (value == null)
            return new ValidationResult("Le code projet est obligatoire");

        string projectCode = value.ToString();
        var model = validationContext.ObjectInstance as ProjectFormViewModel;
        int? currentId = model?.Id;
        // Vérification que le code n'existe pas déjà en base
        var repository = validationContext.GetRequiredService<IProjectRepository>();

        var projectExistsTask = currentId.HasValue
            ? repository.ProjectCodeExists(projectCode, currentId.Value)
            : repository.ProjectCodeExists(projectCode);

        if (projectExistsTask.GetAwaiter().GetResult())
            return new ValidationResult("Ce code projet existe déjà");

        return ValidationResult.Success;
    }
}