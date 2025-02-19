using Data.Entities;

namespace WebApi.Services.Interfaces;

public interface IProjectService
{
    Task<List<ProjectEntity>> GetAllProjectsAsync();
    Task<ProjectEntity?> GetProjectByIdAsync(int id);
    Task<ProjectEntity> CreateProjectAsync(ProjectEntity project);
    Task<bool> UpdateProjectAsync(ProjectEntity project);
    Task<bool> DeleteProjectAsync(int id);
}
