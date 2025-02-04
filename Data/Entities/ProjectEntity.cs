using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata;

namespace Data.Entities;

public class ProjectEntity
{
    [Key]
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public string? Description { get; set; }

    [Column(TypeName = "date")]
    public DateTime StartDate { get; set; }

    [Column(TypeName = "date")]
    public DateTime EndDate { get; set; }

    [ForeignKey("CustomerId")]
    public int CustomerId { get; set; }
    public CustomerEntity Customer { get; set; } = null!;

    [ForeignKey("ProductId")]
    public int ProductId { get; set; }
    public ProductEntity Product { get; set; } = null!;

    [ForeignKey("StatusId")]
    public int StatusId { get; set; }
    public StatusTypeEntity Status { get; set; } = null!;

    [ForeignKey("UserId")]
    public int UserId { get; set; }
    public UserEntity User { get; set; } = null!;   
}
