using Data.Entities;

namespace Data.Repositories.Interfaces;

public interface ICustomerRepository : IRepository<CustomerEntity>
{
    Task<CustomerEntity?> GetByCompanyNumberAsync(string companyNumber);
}
