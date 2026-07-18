namespace MelliMaharat.Wpf;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    // here we are establishing connection with database before the program fully executes
    public App() { }
    
    protected override void OnStartup(StartupEventArgs e)
    {
        try
        {
            var context = AppDbContext;
            context.Migrate();

            if (context.Users.Any())
                return;
            
            context.Users.Add(Admin);
            context.SaveChanges();
        }
        catch (Exception ex)
        {
            Show($"An Error Occured While We Trying to Establish Connection With Database:\n{ex.Message}");
            Shutdown();
            return;
        }
        base.OnStartup(e);
    }
}