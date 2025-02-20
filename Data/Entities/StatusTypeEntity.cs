using System.ComponentModel.DataAnnotations;

namespace Data.Entities
{
    public class StatusTypeEntity
    {
        [Key]
        public int Id { get; set; }
        public string StatusName { get; set; } = null!; // Kan inte bli NULL då det endast finns tre val och gör inte användaren ett val så väljs det automatiskt det som är i rutan vid start.
    }
}
