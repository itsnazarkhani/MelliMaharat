namespace DataForge.Tests.UnitTests;

public class MyFixture
{
    public MyFixture() => new ApplicationDbContextFactory().CreateDbContext().Migrate();
}