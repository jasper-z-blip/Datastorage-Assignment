namespace WebApi.Models;

public class ProjectModel
{
    public int Id { get; set; }
    public string ProjectNumber { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public string StartDate { get; set; } = string.Empty;
    public string EndDate { get; set; } = string.Empty;
    public string CustomerName { get; set; } = string.Empty;
    public int TotalPrice { get; set; }
}
