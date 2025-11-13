namespace DataForge.Tests.UnitTests;

public class InitializeDatabase : IClassFixture<MyFixture>
{
    [Fact]
    public async Task IsDatabaseCreated()
    {
        ApplicationDbContext context = new ApplicationDbContextFactory().CreateDbContext();
        
        await SeedAsync(context);

        var selectionsCount = context.Selections.Count();
        Assert.NotEqual(0, selectionsCount);

        var studentsCount = context.Students.Count();
        Assert.NotEqual(0, studentsCount);

        var lessonsCount = context.Lessons.Count();
        Assert.NotEqual(0, lessonsCount);

        var mastersCount = context.Masters.Count();
        Assert.NotEqual(0, mastersCount);

        var presentationCount = context.Presentations.Count();
        Assert.NotEqual(0, presentationCount);
    }
}
