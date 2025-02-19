using Data.Contexts;
using Data.Entities;
using Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Data.Repositories;

public class ProjectRepository : BaseRepository<ProjectEntity>, IProjectRepository
{
    private readonly DataContext _context;

    public ProjectRepository(DataContext context) : base(context)
    {
        _context = context;
    }

    public async Task<List<ProjectEntity>> GetAllProjectsWithStatusAsync()
    {
        return await _context.Projects.Include(p => p.Status).ToListAsync();
    }

    public async Task<ProjectEntity?> GetProjectWithStatusAsync(int id)
    {
        return await _context.Projects.Include(p => p.Status).FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<string> GenerateProjectNumberAsync()
    {
        return $"P-{DateTime.UtcNow.Year}-{Guid.NewGuid().ToString().Substring(0, 4)}";
    }

    public async Task<IDbContextTransaction> BeginTransactionAsync()
    {
        return await _context.Database.BeginTransactionAsync();
    }

    public async Task AddProjectWithTransactionAsync(ProjectEntity project, IDbContextTransaction transaction)
    {
        try
        {
            await _context.Projects.AddAsync(project);
            await _context.SaveChangesAsync();
            await transaction.CommitAsync(); // Här bekräftas transaktionen.
        }
        catch
        {
            await transaction.RollbackAsync(); // Här sker Rollback vid fel.
            throw;
        }
    }
}

