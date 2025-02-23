using WebApi.Helpers;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;

namespace WebApi.Models;

public class CreateProjectModel
{
    [Required(ErrorMessage = "Titel är obligatorisk")]
    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    [Required(ErrorMessage = "Startdatum är obligatoriskt")]
    [JsonConverter(typeof(DateOnlyJsonConverter))]
    public DateOnly StartDate { get; set; }

    [Required(ErrorMessage = "Slutdatum är obligatoriskt")]
    [JsonConverter(typeof(DateOnlyJsonConverter))]
    public DateOnly EndDate { get; set; }

    public int CustomerId { get; set; }
    public int ProductId { get; set; }
    public int StatusId { get; set; }
    public int UserId { get; set; }

    public string? ProjectNumber { get; set; }
}