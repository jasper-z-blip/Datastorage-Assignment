using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;

namespace Data.Contexts; //Kommentar till mig själv: Talar om vilken databas och hur DataContext konfigureras. EF Core kan jobba mot databasen och restrerande kod behöver ej köras.

public class DataContextFactory : IDesignTimeDbContextFactory<DataContext>
{
    public DataContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<DataContext>();

        optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=MyLocalDB;Trusted_Connection=True;Connect Timeout=30");

        return new DataContext(optionsBuilder.Options);
    }
}
