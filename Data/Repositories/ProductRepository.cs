using Data.Contexts;

namespace Data.Repositories;

public class ProductRepository : BaseRepository<ProductEntity>
{
    public ProductRepository(DataContext context) : base(context) { }

}
