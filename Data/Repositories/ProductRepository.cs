using Data.Contexts;
using Data.Entities;
using Data.Repositories.Interfaces;

namespace Data.Repositories;

public class ProductRepository : BaseRepository<ProductEntity>
{
    public ProductRepository(DataContext context) : base(context) { }

}
