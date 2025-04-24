using System.ComponentModel.DataAnnotations;

namespace Condorcet.B2.ProjectManagement.WebApplication.Models.Projects;

public class ProjectDetailsViewModel
{
    public int Id { get; set; }
    [Display(Name = "Titre du projet")] public string Title { get; set; }

    [Display(Name = "Code du projet")] public string ProjectCode { get; set; }

    [Display(Name = "Description")] public string? Description { get; set; }

    [Display(Name = "Date de début")] public DateTime StartDate { get; set; }

    [Display(Name = "Date de fin prévue")] public DateTime? ExpectedEndDate { get; set; }

    [Display(Name = "Priorité du projet")] public ProjectPriority Priority { get; set; }

    [Display(Name = "Budget")] public decimal Budget { get; set; }
}