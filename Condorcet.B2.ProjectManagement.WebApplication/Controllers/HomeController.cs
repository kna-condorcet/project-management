using System.Diagnostics;
using Condorcet.B2.ProjectManagement.WebApplication.Core.Repository;
using Microsoft.AspNetCore.Mvc;
using Condorcet.B2.ProjectManagement.WebApplication.Models;
using Condorcet.B2.ProjectManagement.WebApplication.Models.Home;
using Condorcet.B2.ProjectManagement.WebApplication.Models.Projects;

namespace Condorcet.B2.ProjectManagement.WebApplication.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IProjectRepository _projectRepository;

    public HomeController(ILogger<HomeController> logger, IProjectRepository projectRepository)
    {
        _logger = logger;
        _projectRepository = projectRepository;
    }

    public async Task<IActionResult> Index()
    {
        var viewModel = new HomeViewModel
        {
            TotalProjects = await _projectRepository.GetTotalProjectsCountAsync(),
            ActiveProjectsCount = await _projectRepository.GetActiveProjectsCountAsync(),
            UpcomingProjectsCount = await _projectRepository.GetUpcomingProjectsCountAsync(),
            CriticalProjectsCount = await _projectRepository.GetCriticalProjectsCountAsync(),
            TotalBudget = await _projectRepository.GetTotalBudgetAsync(),
            RecentProjects = (await _projectRepository.GetRecentProjectsAsync(5))
                .Select(project => new ProjectDetailsViewModel()
                {
                    Id = project.Id,
                    ProjectCode = project.ProjectCode,
                    Title = project.Title,
                    Description = project.Description,
                    StartDate = project.StartDate,
                    ExpectedEndDate = project.ExpectedEndDate,
                    Priority = (ProjectPriority)project.Priority,
                    Budget = project.Budget
                })
        };

        return View(viewModel);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
