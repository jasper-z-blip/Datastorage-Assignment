using System.Text.Json.Serialization;
using WebApi.Helpers;

namespace WebApi.Models;

public class UpdateProjectModel
{
    public int Id { get; set; }
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
    public string ProjectNumber { get; set; } = null!;
}
