using System.ComponentModel.DataAnnotations;

namespace Condorcet.B2.ProjectManagement.WebApplication.Models.Projects;

public enum ProjectPriority
{
    [Display(Name = "Critique")]
    Critical = 1,
    
    [Display(Name = "Haute")]
    High = 2,
    
    [Display(Name = "Moyenne")]
    Medium = 3,
    
    [Display(Name = "Basse")]
    Low = 4,
    
    [Display(Name = "Optionnelle")]
    Optional = 5
}