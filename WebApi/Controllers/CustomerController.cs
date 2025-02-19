using WebApi.Services;
using WebApi.Services.Interfaces;
using Data.Entities;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("api/customers")]
public class CustomerController : ControllerBase
{
    private readonly ICustomerService _customerService;

    public CustomerController(ICustomerService customerService)
    {
        _customerService = customerService;
    }

    [HttpGet]
    public async Task<IActionResult> GetCustomers()
    {
        try
        {
            var customers = await _customerService.GetAllCustomersAsync();
            return Ok(customers);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Ett internt serverfel uppstod: {ex.Message}");
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetCustomerById(int id)
    {
        try
        {
            var customer = await _customerService.GetCustomerByIdAsync(id);
            if (customer == null)
                return NotFound($"Ingen kund hittades med ID {id}.");

            return Ok(customer);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Ett internt serverfel uppstod: {ex.Message}");
        }
    }

    [HttpPost]
    public async Task<IActionResult> AddCustomer([FromBody] CustomerEntity customer)
    {
        if (customer == null)
            return BadRequest("Kunddata är obligatorisk.");

        if (string.IsNullOrWhiteSpace(customer.FirstName) ||
            string.IsNullOrWhiteSpace(customer.LastName) ||
            string.IsNullOrWhiteSpace(customer.CompanyName) ||
            string.IsNullOrWhiteSpace(customer.CompanyNumber))
        {
            return BadRequest("Alla obligatoriska fält måste fyllas i (förnamn, efternamn, företagsnamn, företagsnummer).");
        }

        try
        {
            var newCustomer = await _customerService.AddCustomerAsync(customer);
            return CreatedAtAction(nameof(GetCustomerById), new { id = newCustomer.Id }, newCustomer);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Ett internt serverfel uppstod: {ex.Message}");
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCustomer(int id, [FromBody] CustomerEntity updatedCustomer)
    {
        if (updatedCustomer == null || id != updatedCustomer.Id)
            return BadRequest("Felaktiga kunduppgifter.");

        try
        {
            var existingCustomer = await _customerService.GetCustomerByIdAsync(id);
            if (existingCustomer == null)
                return NotFound($"Ingen kund hittades med ID {id}.");

            existingCustomer.FirstName = updatedCustomer.FirstName ?? existingCustomer.FirstName;
            existingCustomer.LastName = updatedCustomer.LastName ?? existingCustomer.LastName;
            existingCustomer.CompanyName = updatedCustomer.CompanyName ?? existingCustomer.CompanyName;
            existingCustomer.Address = updatedCustomer.Address ?? existingCustomer.Address;
            existingCustomer.CompanyNumber = updatedCustomer.CompanyNumber ?? existingCustomer.CompanyNumber;

            await _customerService.UpdateCustomerAsync(existingCustomer);
            return Ok(existingCustomer);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Ett internt serverfel uppstod: {ex.Message}");
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCustomer(int id)
    {
        try
        {
            var customer = await _customerService.GetCustomerByIdAsync(id);
            if (customer == null)
                return NotFound($"Ingen kund hittades med ID {id}.");

            await _customerService.DeleteCustomerAsync(id);
            return NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Ett internt serverfel uppstod: {ex.Message}");
        }
    }
}
