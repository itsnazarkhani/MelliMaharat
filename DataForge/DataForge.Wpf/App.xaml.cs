namespace DataForge.Wpf;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    // this construction establishes connection with database before the program totally executes
    // if you want to ensure database is also Update With Latest Changes use .Migrate() instead of .Create()
    public App() { }
    
    protected override void OnStartup(StartupEventArgs e)
    {
        try
        {
            new ApplicationDbContextFactory().CreateDbContext().Migrate();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Database Initialization Failed:\n{ex.Message}");
            Shutdown();
            return;
        }

        base.OnStartup(e);
        string userMode = (e.Args.Length > 0 && e.Args[0].Equals("admin", StringComparison.OrdinalIgnoreCase))
                              ? "admin" 
                              : "guest";

        Current.Properties["usermode"] = userMode;
    }
}