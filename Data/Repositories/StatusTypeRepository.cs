using Data.Contexts;
using Data.Entities;

namespace Data.Repositories;

public class StatusTypeRepository : BaseRepository<StatusTypeEntity>
{
    public StatusTypeRepository(DataContext context) : base(context) { }
}

