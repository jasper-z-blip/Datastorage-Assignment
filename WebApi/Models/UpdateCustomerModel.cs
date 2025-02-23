namespace WebApi.Models;

public class UpdateCustomerModel
{
    public int Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string CompanyName { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string CompanyNumber { get; set; } = string.Empty;
}
