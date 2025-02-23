using WebApi.Helpers;
using WebApi.Services.Interfaces;
using Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace WebApi.Services;

public class ProjectService : IProjectService
{
    private readonly IProjectRepository _projectRepository;

    public ProjectService(IProjectRepository projectRepository)
    {
        _projectRepository = projectRepository;
    }

    public async Task<List<ProjectEntity>> GetAllProjectsAsync()
    {
        return await _projectRepository.GetAllProjectsWithStatusAsync();
    }

    public async Task<ProjectEntity?> GetProjectByIdAsync(int id)
    {
        return await _projectRepository.GetProjectWithStatusAsync(id);
    }

    public async Task<ProjectEntity> CreateProjectAsync(ProjectEntity project)
    {
        // Hjälp av chatGPT, Startar transationen, await using gör att transaktionen stängs korrekt när blocket är klart.
        await using var transaction = await _projectRepository.BeginTransactionAsync();

        try
        {
            if (string.IsNullOrWhiteSpace(project.ProjectNumber))
            {
                project.ProjectNumber = await _projectRepository.GenerateProjectNumberAsync();
            }

            project.TotalPrice = CalculateTotalPrice(project.StartDate, project.EndDate, project.ProductId);

            await _projectRepository.AddProjectWithTransactionAsync(project, transaction);

            return project;
        }
        // Om databasspecifikt fel uppstår görs en rollback här.
        catch (DbUpdateException)
        {
            await transaction.RollbackAsync();
            throw;
        }
        catch (Exception ex)
        // Om något annat fel uppstår görs en rollback här.
        {
            await transaction.RollbackAsync();
            throw new Exception("Ett fel uppstod vid skapandet av projektet.", ex);
        }
    }

    public async Task<bool> UpdateProjectAsync(ProjectEntity project)
    {
        var existingProject = await _projectRepository.GetByIdAsync(project.Id);
        if (existingProject == null)
            return false;

        existingProject.Title = project.Title;
        existingProject.Description = project.Description;
        existingProject.StartDate = project.StartDate;
        existingProject.EndDate = project.EndDate;
        existingProject.CustomerId = project.CustomerId;
        existingProject.ProductId = project.ProductId;
        existingProject.StatusId = project.StatusId;
        existingProject.UserId = project.UserId;

        existingProject.ProjectNumber ??= await _projectRepository.GenerateProjectNumberAsync();

        existingProject.TotalPrice = CalculateTotalPrice(existingProject.StartDate, existingProject.EndDate, existingProject.ProductId);

        await _projectRepository.UpdateAsync(existingProject);
        return true;
    }

    public async Task<bool> DeleteProjectAsync(int id)
    {
        return await _projectRepository.DeleteAsync(id);
    }

    private int CalculateTotalPrice(DateOnly startDate, DateOnly endDate, int productId)
    {
        int businessDays = ComputeBusinessDays(startDate, endDate);
        int dailyRate = GetRateByProductId(productId);
        return businessDays * dailyRate;
    }

    private int ComputeBusinessDays(DateOnly startDate, DateOnly endDate)
    {
        return CalculateOnlyBusinessDays.GetBusinessDays(startDate, endDate);
    }

    private int GetRateByProductId(int productId)
    {
        return productId switch // Switch för att ev. kuna lägga till fler tjänster senare.
        {
            1 => 4000,  // Junior Developer
            2 => 6000,  // Midlevel Developer
            3 => 9600,  // Senior Developer
            _ => 0      // Okänd produkt
        };
    }
}
