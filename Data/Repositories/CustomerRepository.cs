using Data.Contexts;
using Data.Entities;
using Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories;

public class CustomerRepository : BaseRepository<CustomerEntity>, ICustomerRepository
{
    public CustomerRepository(DataContext context) : base(context) { }

    public async Task<CustomerEntity?> GetByCompanyNumberAsync(string companyNumber)
    {
        return await _context.Customers
            .FirstOrDefaultAsync(c => c.CompanyNumber == companyNumber);
    }
}
