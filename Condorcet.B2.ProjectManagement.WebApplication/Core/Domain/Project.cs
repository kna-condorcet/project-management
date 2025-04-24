namespace Condorcet.B2.ProjectManagement.WebApplication.Core.Domain;

public class Project
{
    public int Id { get; set; }
    public required string Title { get; set; }
    public required string ProjectCode { get; set; }
    public string? Description { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? ExpectedEndDate { get; set; }
    public int Priority { get; set; }
    public decimal Budget { get; set; }
}