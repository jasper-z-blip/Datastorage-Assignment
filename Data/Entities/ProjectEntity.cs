using Data.Entities;
using Data.Helpers;

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
                // Anropar metoden för att generera Projektnummer automatiskt.
                _projectNumber = ProjectNumberGenerator.GenerateProjectNumber(); 
            }
            return _projectNumber;
        }
        //Värdet är oförändrligt för användaren men kan skrivas över om ett värde redan finns.
        set => _projectNumber = value;
    }

    // Sparar total priset för projektet. Värdet beräknas senare beroende på antal dagar och vilken tjänst.
    public int TotalPrice { get; set; }   
}
// Där jag tagit hjälp av chatGPT, där sätter jag förklaringar, alltså där jag inte skrivit koden själv.