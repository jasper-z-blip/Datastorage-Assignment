namespace Data.Factories
{
    public static class ProjectFactory
    {
        public static ProjectEntity CreateProject(string title, string description, DateOnly startDate, DateOnly endDate, int customerId, int productId, int statusId, int userId)
        {
            return new ProjectEntity
            {
                Title = title,
                Description = description,
                StartDate = startDate,
                EndDate = endDate,
                CustomerId = customerId,
                ProductId = productId,
                StatusId = statusId,
                UserId = userId,
                ProjectNumber = GenerateProjectNumber(),
                TotalPrice = 0 // Varför 0? Räknas ut senare.
            };
        }

        // Generera automatiskt projektnummer: P-2025-XXXXX Flytta till Helpers?
        private static string GenerateProjectNumber()
        {
            return $"P-{DateTime.UtcNow.Year}-{Guid.NewGuid().ToString().Substring(0, 5)}";
        }
    }
}
