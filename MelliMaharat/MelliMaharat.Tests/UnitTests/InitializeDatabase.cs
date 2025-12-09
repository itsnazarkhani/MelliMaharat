namespace MelliMaharat.Tests.UnitTests;

public class InitializeDatabase : IClassFixture<MyFixture>
{
    [Fact]
    public async Task IsDatabaseCreated()
    {
        ApplicationDbContext context = new ApplicationDbContextFactory().CreateDbContext();
        if (context.Create())
        {
            context.Migrate();
            await SeedAsync(context);
        }

        var _admin = await context.Users.SingleOrDefaultAsync(x => x.Username == "admin");
        Assert.NotNull(_admin);

        var studentsCount = await context.Students.CountAsync();
        Assert.NotEqual(0, studentsCount);

        var mastersCount = await context.Masters.CountAsync();
        Assert.NotEqual(0, mastersCount);

        var departmentCount = await context.Departments.CountAsync();
        Assert.NotEqual(0, departmentCount);

        var lessonsCount = await context.Lessons.CountAsync();
        Assert.NotEqual(0, lessonsCount);

        var presentationCount = await context.Presentations.CountAsync();
        Assert.NotEqual(0, presentationCount);

        var selectionsCount = await context.Selections.CountAsync();
        Assert.NotEqual(0, selectionsCount);

        var termsCount = await context.Terms.CountAsync();
        Assert.NotEqual(0, termsCount);
    }
}