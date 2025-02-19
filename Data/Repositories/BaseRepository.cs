using Data.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories;

public class BaseRepository<T> where T : class
{
    protected readonly DataContext _context;
    private readonly DbSet<T> _dbSet;

    public BaseRepository(DataContext context)
    {
        _context = context;
        _dbSet = context.Set<T>();
    }

    public async Task<List<T>> GetAllAsync()
    {
        return await _dbSet.ToListAsync();
    }

    public async Task<T?> GetByIdAsync(int id)
    {
        return await _dbSet.FindAsync(id);
    }

    public async Task AddAsync(T entity)
    {
        _dbSet.Add(entity);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(T entity)
    {
        _dbSet.Update(entity);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var entity = await _dbSet.FindAsync(id);
        if (entity == null) return false; // Returnera false om objektet inte finns.

        _dbSet.Remove(entity);
        await _context.SaveChangesAsync();
        return true; // Returnera true om borttagningen lyckades.
    }
}