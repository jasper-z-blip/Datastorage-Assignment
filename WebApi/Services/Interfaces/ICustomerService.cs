using Data.Entities;

namespace WebApi.Services.Interfaces;

public interface ICustomerService
{
    Task<List<CustomerEntity>> GetAllCustomersAsync();
    Task<CustomerEntity?> GetCustomerByIdAsync(int id);
    Task<CustomerEntity> AddCustomerAsync(CustomerEntity customer);
    Task UpdateCustomerAsync(CustomerEntity customer);
    Task<bool> DeleteCustomerAsync(int id);
}
