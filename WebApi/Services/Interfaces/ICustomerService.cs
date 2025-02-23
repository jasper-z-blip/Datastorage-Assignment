using Data.Entities;
using WebApi.Models;

namespace WebApi.Services.Interfaces;

public interface ICustomerService
{
    Task<List<CustomerModel>> GetAllCustomersAsync();
    Task<CustomerEntity?> GetCustomerByIdAsync(int id);
    Task<CustomerEntity> AddCustomerAsync(CustomerEntity customer);
    Task UpdateCustomerAsync(CustomerEntity customer);
    Task<bool> DeleteCustomerAsync(int id);
}
