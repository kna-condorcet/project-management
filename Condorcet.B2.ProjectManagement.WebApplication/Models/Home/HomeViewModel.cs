using Condorcet.B2.ProjectManagement.WebApplication.Models.Projects;

namespace Condorcet.B2.ProjectManagement.WebApplication.Models.Home;

public class HomeViewModel
{
    public int TotalProjects { get; set; }
    public int ActiveProjectsCount { get; set; }
    public int UpcomingProjectsCount { get; set; }
    public int CriticalProjectsCount { get; set; }
    public decimal TotalBudget { get; set; }
    public IEnumerable<ProjectDetailsViewModel> RecentProjects { get; set; } = new List<ProjectDetailsViewModel>();
}