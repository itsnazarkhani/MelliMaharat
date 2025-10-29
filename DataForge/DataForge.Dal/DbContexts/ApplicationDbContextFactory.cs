namespace DataForge.Dal.DbContexts;

public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
{
    IConfigurationRoot configFile = new ConfigurationBuilder()
                                            .SetBasePath(GetCurrentDirectory())
                                            .AddJsonFile("appsettings.configuration.json", true, true)
                                            .Build();

    public ApplicationDbContext CreateDbContext(string[] args = null)
    {
        string connectionString = configFile.GetConnectionString("DataForge");
        DbContextOptionsBuilder<ApplicationDbContext> optionsBuilder = new();
        optionsBuilder.UseSqlServer(connectionString);
        DbContextOptions<ApplicationDbContext> options = optionsBuilder.Options;
        WriteLine($"Your Connection String:\n\t{connectionString}\n");
        return new ApplicationDbContext(options);
    }
}