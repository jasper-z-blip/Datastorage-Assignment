using Data.Entities;

public class ProjectEntity
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateOnly StartDate { get; set; }
    public DateOnly EndDate { get; set; }
    public int StatusId { get; set; }

    public StatusTypeEntity Status { get; set; }
    public int CustomerId { get; set; }
    public int ProductId { get; set; }
    public int UserId { get; set; }

    private string? _projectNumber;
    public string ProjectNumber
    {
        get
        {
            if (_projectNumber == null)
            {
                _projectNumber = GenerateProjectNumber();
            }
            return _projectNumber;
        }
        set => _projectNumber = value;
    }

    public int TotalPrice { get; set; }

    // Här genererar vi projektnumret baserat på antalet projekt i databasen. Flytta till en Helpers?
    private string GenerateProjectNumber()
    {
        return $"P-{DateTime.UtcNow.Year}-{Guid.NewGuid().ToString().Substring(0, 5)}";
    }
}
