namespace DataForge.Tests.UnitTests;

public class MyFixture
{
    public MyFixture()
    {
        var context = new ApplicationDbContextFactory().CreateDbContext();
        context.Database.Migrate();
    }
}