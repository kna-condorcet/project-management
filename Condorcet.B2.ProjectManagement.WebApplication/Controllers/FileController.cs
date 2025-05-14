using Condorcet.B2.ProjectManagement.WebApplication.Models.File;
using Microsoft.AspNetCore.Mvc;

namespace Condorcet.B2.ProjectManagement.WebApplication.Controllers;

public class FileController : Controller
{
    private readonly IWebHostEnvironment _environment;

    public FileController(IWebHostEnvironment environment)
    {
        _environment = environment;
    }

    public IActionResult Upload()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Upload(FileUploadModel model)
    {
        if (ModelState.IsValid)
        {
            try
            {
                if (model.File.Length > 0)
                {
                    // Create upload directory if it doesn't exist
                    string uploadsFolder = Path.Combine(_environment.WebRootPath, "uploads");
                    if (!Directory.Exists(uploadsFolder))
                    {
                        Directory.CreateDirectory(uploadsFolder);
                    }

                    // Generate unique filename
                    string uniqueFileName = Guid.NewGuid() + "_" + Path.GetFileName(model.File.FileName);
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    // Save file
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await model.File.CopyToAsync(fileStream);
                    }

                    TempData["SuccessMessage"] = "File uploaded successfully!";

                    return RedirectToAction(nameof(Upload));
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"File upload failed: {ex.Message}");
            }
        }

        return View(model);
    }
}