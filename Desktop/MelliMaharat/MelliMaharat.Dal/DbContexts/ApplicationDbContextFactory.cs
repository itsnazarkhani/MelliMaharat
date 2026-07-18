using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace MelliMaharat.Dal.DbContexts;

public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
{
<<<<<<< HEAD:Desktop/MelliMaharat/MelliMaharat.Dal/DbContexts/ApplicationDbContextFactory.cs
    readonly IConfigurationRoot configFile = new ConfigurationBuilder()
                                            .SetBasePath(GetCurrentDirectory())
                                            .AddJsonFile("appsettings.configuration.json", true, true)
                                            .Build();

    public ApplicationDbContext CreateDbContext(string[] args = null)
=======
    public ApplicationDbContext CreateDbContext(string[] args)
>>>>>>> main:MelliMaharat/MelliMaharat.Dal/DbContexts/ApplicationDbContextFactory.cs
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