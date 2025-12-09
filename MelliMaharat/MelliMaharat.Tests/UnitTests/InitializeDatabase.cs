namespace MelliMaharat.Tests.UnitTests;

public class InitializeDatabase : IClassFixture<MyFixture>
{
    [Fact]
    public async Task IsDatabaseCreated()
    {
        ApplicationDbContext context = new ApplicationDbContextFactory().CreateDbContext();
        
        await SeedAsync(context);

        var _admin = context.Users.SingleOrDefaultAsync(x => x.Username == "admin");
        Assert.NotNull(_admin);

        var studentsCount = context.Students.Count();
        Assert.NotEqual(0, studentsCount);

        var mastersCount = context.Masters.Count();
        Assert.NotEqual(0, mastersCount);

        var departmentCount = context.Departments.Count();
        Assert.NotEqual(0, departmentCount);

        var lessonsCount = context.Lessons.Count();
        Assert.NotEqual(0, lessonsCount);

        var presentationCount = context.Presentations.Count();
        Assert.NotEqual(0, presentationCount);

        var selectionsCount = context.Selections.Count();
        Assert.NotEqual(0, selectionsCount);

        var termsCount = context.Terms.Count();
        Assert.NotEqual(0, termsCount);
    }
}