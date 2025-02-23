using WebApi.Models;
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
            // Hämtar alla customers från databasen.
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
        // Vid oväntat fel så kommer detta felmeddelande skickas tillbaka i console.
        catch (Exception ex)
        {
            return StatusCode(500, $"Ett internt serverfel uppstod: {ex.Message}");
        }
    }

    [HttpPost]
    public async Task<IActionResult> AddCustomer([FromBody] CreateCustomerModel customer)
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
            var customerEntity = new CustomerEntity
            {
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                CompanyName = customer.CompanyName,
                Address = customer.Address,
                CompanyNumber = customer.CompanyNumber
            };

            // Skickar `CustomerEntity` till databasen.
            var newCustomer = await _customerService.AddCustomerAsync(customerEntity);

            return CreatedAtAction(nameof(GetCustomerById), new { id = newCustomer.Id }, newCustomer);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Ett internt serverfel uppstod: {ex.Message}");
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCustomer(int id, [FromBody] UpdateCustomerModel updatedCustomer)
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
// Detta är till viss del en mall tagen från chatGPT, jag har ändrat om mycket men sätter förklaringar där jag inte skrivit koden själv.