using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.Sqlite;
using Data.Contexts;
using Data.Entities;
using Data.Repositories;
using Data.Repositories.Interfaces;
using WebApi.Services;
using WebApi.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

public class ProjectServiceTests : IDisposable
{
    private readonly DataContext _context;
    private readonly IProjectRepository _projectRepository;
    private readonly IProjectService _projectService;
    private readonly SqliteConnection _connection;

    public ProjectServiceTests()
    {
        // Skapa en in-memory SQLite databas
        _connection = new SqliteConnection("DataSource=:memory:");
        _connection.Open();

        var options = new DbContextOptionsBuilder<DataContext>()
            .UseSqlite(_connection) // Använd SQLite för tester
            .Options;

        _context = new DataContext(options);
        _context.Database.EnsureCreated(); // Skapa databasen i minnet

        _projectRepository = new ProjectRepository(_context);
        _projectService = new ProjectService(_projectRepository);
    }

    [Fact]
    public async Task CreateProjectAsync_Should_CommitTransaction_When_Successful()
    {
        // Arrange
        var project = new ProjectEntity
        {
            ProjectNumber = "P-2025-TEST",
            Title = "Test Project",
            Description = "This is a test project",
            StartDate = DateOnly.FromDateTime(DateTime.Today),
            EndDate = DateOnly.FromDateTime(DateTime.Today.AddDays(5)),
            ProductId = 1,
            StatusId = 1,
            CustomerId = 1,
            UserId = 1
        };

        // Act
        var createdProject = await _projectService.CreateProjectAsync(project);
        var retrievedProject = await _projectService.GetProjectByIdAsync(createdProject.Id);

        // Assert
        retrievedProject.Should().NotBeNull();
        retrievedProject.Title.Should().Be("Test Project");
        retrievedProject.ProjectNumber.Should().Be("P-2025-TEST");
    }

    [Fact]
    public async Task CreateProjectAsync_Should_Rollback_When_ExceptionOccurs()
    {
        // Arrange
        var invalidProject = new ProjectEntity
        {
            ProjectNumber = "P-2025-ROLLBACK",
            Title = null, // Detta kommer att orsaka ett valideringsfel
            StartDate = DateOnly.FromDateTime(DateTime.Today),
            EndDate = DateOnly.FromDateTime(DateTime.Today.AddDays(5)),
            ProductId = 1,
            StatusId = 1,
            CustomerId = 1,
            UserId = 1
        };

        // Act
        Func<Task> action = async () => await _projectService.CreateProjectAsync(invalidProject);

        // Assert
        await action.Should().ThrowAsync<Exception>().Where(e =>
            e is DbUpdateException ||
            e is InvalidOperationException);  // Kontrollera att rätt exception kastas

        var projectFromDb = await _context.Projects.FirstOrDefaultAsync(p => p.ProjectNumber == "P-2025-ROLLBACK");
        projectFromDb.Should().BeNull(); // Projektet ska INTE finnas i databasen eftersom det ska rollbackas
    }

    public void Dispose()
    {
        _context.Dispose();
        _connection.Dispose();
    }
}
