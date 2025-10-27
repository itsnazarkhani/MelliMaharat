namespace DataForge.Dal.DbContexts
{
    public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        SqlConnectionStringBuilder connectionBuilder = new SqlConnectionStringBuilder()
        {
            DataSource = @"(localdb)\MSSQLLocalDB",
            InitialCatalog = "DataForge",
            IntegratedSecurity = true
        };
        IConfigurationRoot configFile = new ConfigurationBuilder()
                                    .SetBasePath(Directory.GetCurrentDirectory())
                                    .AddJsonFile("appsettings.configuration.json", true, true)
                                    .Build();
        public ApplicationDbContext CreateDbContext(string[] args = null)
        {
            //string connectionString = connectionBuilder.ConnectionString;

            string connectionString = configFile.GetConnectionString("DataForge");
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseSqlServer(connectionString);
            var options = optionsBuilder.Options;
            Console.WriteLine($"Your Connection String:\n\t{connectionString}\n");
            return new ApplicationDbContext(options);
        }
    }
}
