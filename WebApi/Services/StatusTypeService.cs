using Data.Entities;
using Data.Repositories;

namespace WebApi.Services;

public class StatusTypeService
{
    private readonly StatusTypeRepository _statusTypeRepository;

    public StatusTypeService(StatusTypeRepository statusTypeRepository)
    {
        _statusTypeRepository = statusTypeRepository;
    }

    public async Task<List<StatusTypeEntity>> GetAllStatusTypesAsync()
    {
        return await _statusTypeRepository.GetAllAsync();
    }

    public async Task<StatusTypeEntity?> GetStatusTypeByIdAsync(int id)
    {
        return await _statusTypeRepository.GetByIdAsync(id);
    }

    public async Task AddStatusTypeAsync(StatusTypeEntity statusType)
    {
        await _statusTypeRepository.AddAsync(statusType);
    }

    public async Task UpdateStatusTypeAsync(StatusTypeEntity statusType)
    {
        await _statusTypeRepository.UpdateAsync(statusType);
    }

    public async Task<bool> DeleteStatusTypeAsync(int id)
    {
        var statusType = await _statusTypeRepository.GetByIdAsync(id);
        if (statusType == null) return false;

        await _statusTypeRepository.DeleteAsync(id);
        return true;
    }
}
