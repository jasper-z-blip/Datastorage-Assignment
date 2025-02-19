using Data.Contexts;
using Data.Entities;

namespace Data.Repositories;

public class UserRepository : BaseRepository<UserEntity>
{
    public UserRepository(DataContext context) : base(context) { }
}