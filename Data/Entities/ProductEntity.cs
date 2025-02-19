using System.ComponentModel.DataAnnotations;

public class ProductEntity
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string Name { get; set; } = string.Empty;
}
