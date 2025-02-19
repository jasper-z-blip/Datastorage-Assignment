using WebApi.Models;
using WebApi.Services.Interfaces;
using Data.Entities;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("api/projects")]
public class ProjectController : ControllerBase
{
    private readonly IProjectService _projectService;

    public ProjectController(IProjectService projectService)
    {
        _projectService = projectService;
    }

    [HttpGet]
    public async Task<IActionResult> GetProjects()
    {
        try
        {
            var projects = await _projectService.GetAllProjectsAsync();
            return Ok(projects);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Ett internt serverfel uppstod", error = ex.Message });
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetProjectById(int id)
    {
        var project = await _projectService.GetProjectByIdAsync(id);
        if (project == null)
            return NotFound($"Inget projekt hittades med ID {id}.");

        return Ok(project);
    }

    [HttpPost]
    public async Task<IActionResult> CreateProject([FromBody] CreateProjectModel projectModel)
    {
        if (projectModel == null)
            return BadRequest("Projektdata saknas.");

        var newProject = new ProjectEntity
        {
            Title = projectModel.Title,
            Description = projectModel.Description,
            StartDate = projectModel.StartDate,
            EndDate = projectModel.EndDate,
            CustomerId = projectModel.CustomerId,
            ProductId = projectModel.ProductId,
            StatusId = projectModel.StatusId,
            UserId = projectModel.UserId,
            ProjectNumber = projectModel.ProjectNumber
        };

        var createdProject = await _projectService.CreateProjectAsync(newProject);
        return CreatedAtAction(nameof(GetProjectById), new { id = createdProject.Id }, createdProject);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateProject(int id, [FromBody] UpdateProjectModel updatedProject)
    {
        if (updatedProject == null || id != updatedProject.Id)
            return BadRequest("Felaktiga projektuppgifter.");

        var existingProject = await _projectService.GetProjectByIdAsync(id);
        if (existingProject == null)
            return NotFound($"Inget projekt hittades med ID {id}.");

        existingProject.Title = updatedProject.Title;
        existingProject.Description = updatedProject.Description;
        existingProject.StartDate = DateOnly.FromDateTime(updatedProject.StartDate.ToDateTime(TimeOnly.MinValue));
        existingProject.EndDate = DateOnly.FromDateTime(updatedProject.EndDate.ToDateTime(TimeOnly.MinValue));
        existingProject.CustomerId = updatedProject.CustomerId;
        existingProject.ProductId = updatedProject.ProductId;
        existingProject.StatusId = updatedProject.StatusId;
        existingProject.UserId = updatedProject.UserId;
        existingProject.ProjectNumber = updatedProject.ProjectNumber;

        var result = await _projectService.UpdateProjectAsync(existingProject);
        return result ? Ok(existingProject) : BadRequest("Misslyckades med att uppdatera projekt.");
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProject(int id)
    {
        var project = await _projectService.GetProjectByIdAsync(id);
        if (project == null)
            return NotFound($"Inget projekt hittades med ID {id}.");

        await _projectService.DeleteProjectAsync(id);
        return NoContent();
    }

    [HttpGet("products")]
    public IActionResult GetProducts()
    {
        return Ok(ProductType.Products);
    }
}
