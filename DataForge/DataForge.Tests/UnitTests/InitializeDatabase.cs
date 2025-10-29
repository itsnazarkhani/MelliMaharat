namespace DataForge.Tests.UnitTests
{
    public class InitializeDatabase : IClassFixture<MyFixture>
    {
        [Fact]
        public void IsDatabaseCreated()
        {
            ApplicationDbContext context = new ApplicationDbContextFactory().CreateDbContext();

            var lessonsCount = context.Lessons.ToList().Count;
            Assert.Equal(2, lessonsCount);

            var mastersCount = context.Masters.ToList().Count;
            Assert.Equal(1, mastersCount);

            var presentationCount = context.Presentations.ToList().Count;
            Assert.Equal(1, presentationCount);
        }
    }
}
