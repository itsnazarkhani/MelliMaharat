using DataForge.Dal.DbContexts;

namespace DataForge.Wpf;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    // this construction establishes connection with database before the program totally executes
    // if you want to ensure database is also Update With Latest Changes use .Migrate() instead of .Create()
    public App() => new ApplicationDbContextFactory().CreateDbContext().Migrate();
}