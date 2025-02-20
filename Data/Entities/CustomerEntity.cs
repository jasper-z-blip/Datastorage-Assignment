using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entities;

public class CustomerEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Gör så att ID genereras automatiskt av databasen.
    public int Id { get; set; }

    [Required]
    public string FirstName { get; set; } = string.Empty;

    [Required]
    public string LastName { get; set; } = string.Empty;

    [Required]
    public string CompanyName { get; set; } = string.Empty;

    [Required]
    public string Address { get; set; } = string.Empty;

    [Required]
    public string CompanyNumber { get; set; } = string.Empty;
}
// Requiered = Får ej vara NULL när den sparas i databasen.
// string.Empty = Egenskapen kan inte vara NULL från början men den kan vara en tom sträng om inget anges av användaren.
// Valde att ha bägge, för annars får jag NullReferenceExeption om jag inte har string.Empty.
