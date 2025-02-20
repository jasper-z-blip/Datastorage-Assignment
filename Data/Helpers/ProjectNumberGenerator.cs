namespace Data.Helpers;

public static class ProjectNumberGenerator
{
    // Generera automatiskt projektnummer: P-2025-XXXXX.
    public static string GenerateProjectNumber()
    {
        return $"P-{DateTime.UtcNow.Year}-{Guid.NewGuid().ToString().Substring(0, 5)}";
    }
}
