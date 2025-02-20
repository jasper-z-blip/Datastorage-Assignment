using Microsoft.EntityFrameworkCore;
using Data.Entities;

namespace Data.Contexts; //Kommentar till mig själv: DataContext fungerar som planritning för databasen så att EF Core vet hur den ska skapa och hantera databasen.

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
    }

    public DbSet<CustomerEntity> Customers { get; set; }
    public DbSet<StatusTypeEntity> StatusTypes { get; set; }
    public DbSet<UserEntity> Users { get; set; }
    public DbSet<ProjectEntity> Projects { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer(
                "Server=(localdb)\\mssqllocaldb;Database=MyDatabaseName;Trusted_Connection=True;"
            );
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Hantera DateOnly (sparas som DateTime i SQL Server)
        modelBuilder.Entity<ProjectEntity>()
            .Property(p => p.StartDate)
            .HasConversion(v => v.ToDateTime(TimeOnly.MinValue), v => DateOnly.FromDateTime(v));

        modelBuilder.Entity<ProjectEntity>()
            .Property(p => p.EndDate)
            .HasConversion(v => v.ToDateTime(TimeOnly.MinValue), v => DateOnly.FromDateTime(v));

        modelBuilder.Entity<ProjectEntity>()
            .Property(p => p.ProjectNumber)
            .IsRequired()
            .HasMaxLength(50);

        // Skapar ett unikt vvärde så att inget annat projekt kan få samma nummer.
        modelBuilder.Entity<ProjectEntity>()
            .HasIndex(p => p.ProjectNumber)
            .IsUnique();

        modelBuilder.Entity<StatusTypeEntity>().HasData(
            new StatusTypeEntity { Id = 1, StatusName = "Ej påbörjat" },
            new StatusTypeEntity { Id = 2, StatusName = "Pågående" },
            new StatusTypeEntity { Id = 3, StatusName = "Avslutat" }
        );

        modelBuilder.Entity<CustomerEntity>().HasData(
            new CustomerEntity
            {
                Id = 1,
                FirstName = "John",
                LastName = "Doe",
                CompanyName = "Testföretag AB",
                Address = "Stockholm, Sverige",
                CompanyNumber = "556677-8899"
            }
        );

        // Seed data för Users, tanken är att lägga till en user som får ändra i projektet (om jag hinner).
        modelBuilder.Entity<UserEntity>().HasData(
            new UserEntity { Id = 1, FirstName = "Admin", LastName = "User", Email = "admin@example.com" }
        );

        modelBuilder.Entity<ProjectEntity>()
            .Property(p => p.TotalPrice)
            .HasDefaultValue(0);
    }
}
// Detta är en mall tagen från chatGPT, jag har ändrat om mycket men sätter förklaringar där jag inte skrivit koden själv.

