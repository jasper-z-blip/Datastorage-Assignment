using Data.Contexts;
using Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories;

public class ProjectRepository
{
    private readonly DataContext _context;

    public ProjectRepository(DataContext context)
    {
        _context = context;
    }

    public async Task<List<ProjectEntity>> GetAllProjectAsync()
    {
        return await _context.Projects.Include(p => p.Customer).Include(p => p.Status).ToListAsync();
    }

    public async Task<ProjectEntity?> GetProjectByIdAsync(int id)
    {
        return await _context.Projects.Include(p => p.Customer).Include(p => p.Status).FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<ProjectEntity> CreateProjectAsync(ProjectEntity project)
    {
        Console.WriteLine("Creating project: " + project.Title);
        _context.Projects.Add(project);
        await _context.SaveChangesAsync();
        return project;
    }

    public async Task<bool> UpdateProjectAsync(int id, ProjectEntity updatedProject)
    {
        var existingProject = await _context.Projects.FindAsync(id);
        if (existingProject == null) return false;

        existingProject.Title = updatedProject.Title;
        existingProject.Description = updatedProject.Description;
        existingProject.StartDate = updatedProject.StartDate;
        existingProject.EndDate = updatedProject.EndDate;
        existingProject.StatusId = updatedProject.StatusId;

        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteProjectAsync(int id)
    {
        var project = await _context.Projects.FindAsync(id);
        if (project == null) return false;

        _context.Projects.Remove(project);
        await _context.SaveChangesAsync();
        return true;
    }
}
