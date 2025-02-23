using WebApi.Services.Interfaces;
using Data.Repositories.Interfaces;
using Data.Entities;
using WebApi.Models;

namespace WebApi.Services;

public class CustomerService : ICustomerService
{
    private readonly ICustomerRepository _customerRepository;

    public CustomerService(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    public async Task<List<CustomerModel>> GetAllCustomersAsync()
    {
        var customers = await _customerRepository.GetAllAsync();

        return customers.Select(c => new CustomerModel
        {
            Id = c.Id,
            FirstName = c.FirstName,
            LastName = c.LastName,
            CompanyName = c.CompanyName,
            Address = c.Address
        }).ToList();
    }

    public async Task<CustomerEntity?> GetCustomerByIdAsync(int id)
    {
        return await _customerRepository.GetByIdAsync(id);
    }

    public async Task<CustomerEntity> AddCustomerAsync(CustomerEntity customer)
    {
        await _customerRepository.AddAsync(customer);
        return customer;
    }

    public async Task UpdateCustomerAsync(CustomerEntity customer)
    {
        await _customerRepository.UpdateAsync(customer);
    }

    public async Task<bool> DeleteCustomerAsync(int id)
    {
        var customer = await _customerRepository.GetByIdAsync(id);
        if (customer == null) return false;

        await _customerRepository.DeleteAsync(id);
        return true;
    }
}
