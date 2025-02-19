using WebApi.Helpers;
using System.Text.Json.Serialization;

namespace WebApi.Models;

public class CreateProjectModel
{
    public string Title { get; set; } = null!;
    public string? Description { get; set; }

    [JsonConverter(typeof(DateOnlyJsonConverter))]
    public DateOnly StartDate { get; set; }

    [JsonConverter(typeof(DateOnlyJsonConverter))]
    public DateOnly EndDate { get; set; }

    public int CustomerId { get; set; }
    public int ProductId { get; set; }
    public int StatusId { get; set; }
    public int UserId { get; set; }

    public string? ProjectNumber { get; set; }
}

