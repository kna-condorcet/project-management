using Condorcet.B2.ProjectManagement.WebApplication.Core.Domain;
using Condorcet.B2.ProjectManagement.WebApplication.Core.Repository;
using Condorcet.B2.ProjectManagement.WebApplication.Models;
using Condorcet.B2.ProjectManagement.WebApplication.Models.Projects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Condorcet.B2.ProjectManagement.WebApplication.Controllers;

public class ProjectsController : Controller
{
    private readonly IProjectRepository _projectRepository;

    public ProjectsController(IProjectRepository projectRepository)
    {
        _projectRepository = projectRepository;
    }
    
    public async Task<ActionResult> Index(string? sortOrder, string? priority, string? status)
    {
        var projectsTask = (priority, status) switch
        {
            ("critical", _) => _projectRepository.GetCriticalProjects(),
            (_, "active") => _projectRepository.GetActiveProjects(),
            (_, "upcoming") => _projectRepository.GetUpcomingProjects(),
            _ => _projectRepository.GetAll()
        };
        
        var projects = await projectsTask;
        if (sortOrder != null)
        {
            projects = sortOrder switch
            {
                "date_desc" => projects.OrderByDescending(p => p.StartDate).ToList(),
                "priority_asc" => projects.OrderBy(p => p.Priority).ToList(),
                "budget_desc" => projects.OrderByDescending(p => p.Budget).ToList(),
                _ => projects
            };
        }
        return View(new ProjectIndexViewModel()
        {
            Projects = projects.Select(project => new ProjectDetailsViewModel()
            {
                Id = project.Id,
                ProjectCode = project.ProjectCode,
                Title = project.Title,
                Description = project.Description,
                StartDate = project.StartDate,
                ExpectedEndDate = project.ExpectedEndDate,
                Priority = (ProjectPriority)project.Priority,
                Budget = project.Budget
            }).ToList()
        });
    }

    [Authorize(Policy = "RequireUserRole")]
    public IActionResult Create()
    {
        return View(new ProjectFormViewModel()
        {
            StartDate = DateTime.Today
        });
    }

    public async Task<IActionResult> Details([FromRoute(Name = "id")] int projectId)
    {
        var project = await _projectRepository.GetById(projectId);
        if (project == null)
        {
            return NotFound();
        }

        return View(new ProjectDetailsViewModel()
        {
            Id = project.Id,
            ProjectCode = project.ProjectCode,
            Title = project.Title,
            Description = project.Description,
            StartDate = project.StartDate,
            ExpectedEndDate = project.ExpectedEndDate,
            Priority = (ProjectPriority)project.Priority,
            Budget = project.Budget
        });
    }


    [Authorize(Policy = "RequireUserRole")]
    public async Task<IActionResult> Edit([FromRoute(Name = "id")] int projectId)
    {
        var project = await _projectRepository.GetById(projectId);
        if (project == null)
        {
            return NotFound();
        }

        return View(new ProjectFormViewModel()
        {
            Id = project.Id,
            ProjectCode = project.ProjectCode,
            Title = project.Title,
            Description = project.Description,
            StartDate = project.StartDate,
            ExpectedEndDate = project.ExpectedEndDate,
            Priority = (ProjectPriority)project.Priority,
            Budget = project.Budget
        });
    }

    [HttpPost]
    [Authorize(Policy = "RequireUserRole")]
    public async Task<IActionResult> Create(ProjectFormViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View();
        }

        await _projectRepository.Insert(new Project
        {
            Title = model.Title,
            Description = model.Description,
            StartDate = model.StartDate,
            ExpectedEndDate = model.ExpectedEndDate,
            Priority = (int)model.Priority,
            Budget = model.Budget,
            ProjectCode = model.ProjectCode
        });
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [Authorize(Policy = "RequireUserRole")]
    public async Task<IActionResult> Edit(ProjectFormViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View();
        }

        //TODO Update
        return RedirectToAction(nameof(Index));
    }
    
    [Authorize(Policy = "RequireAdminRole")]
    public async Task<IActionResult> Delete(int id)
    {
        var project = await _projectRepository.GetById(id);
        if (project == null)
        {
            return NotFound();
        }

        return View(new ProjectDetailsViewModel()
        {
            Id = project.Id,
            ProjectCode = project.ProjectCode,
            Title = project.Title,
            Description = project.Description,
            StartDate = project.StartDate,
            ExpectedEndDate = project.ExpectedEndDate,
            Priority = (ProjectPriority)project.Priority,
            Budget = project.Budget
        });
    }
    
    [HttpPost]
    [Authorize(Policy = "RequireAdminRole")]
    public async Task<IActionResult> Delete(ProjectDeleteViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View();
        }
        var project = await _projectRepository.GetById(model.Id);
        if (project == null)
        {
            return NotFound();
        }

        await _projectRepository.Delete(project.Id);

        return RedirectToAction(nameof(Index));
    }
}