using System.ComponentModel.DataAnnotations;

namespace Condorcet.B2.ProjectManagement.WebApplication.Models.File;

public class FileUploadModel
{
    [Required(ErrorMessage = "Please select a file")]
    [Display(Name = "File")]
    public IFormFile File { get; set; }
        
    [Display(Name = "Description")]
    public string Description { get; set; }
}