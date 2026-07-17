using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace MelliMaharat.Dal.DbContexts;

public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
{
    public ApplicationDbContext CreateDbContext(string[] args)
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Path.Combine(
                Directory.GetCurrentDirectory(),
                "../MelliMaharat.Web"
            ))
            .AddJsonFile("appsettings.json")
            .Build();

        var connectionString =
            configuration.GetConnectionString("MelliMaharat");

        var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();

        optionsBuilder.UseSqlServer(connectionString);

        return new ApplicationDbContext(optionsBuilder.Options);
    }
}