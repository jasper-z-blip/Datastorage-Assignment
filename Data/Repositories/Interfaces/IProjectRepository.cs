using Microsoft.EntityFrameworkCore.Storage;
using Data.Entities;

namespace Data.Repositories.Interfaces;

public interface IProjectRepository : IRepository<ProjectEntity>
{
    Task<List<ProjectEntity>> GetAllProjectsWithStatusAsync();
    Task<ProjectEntity?> GetProjectWithStatusAsync(int id);
    Task<string> GenerateProjectNumberAsync();
    Task<IDbContextTransaction> BeginTransactionAsync();
    Task AddProjectWithTransactionAsync(ProjectEntity project, IDbContextTransaction transaction);
}

