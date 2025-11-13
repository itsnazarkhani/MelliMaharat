namespace DataForge.Tests.UnitTests;

public class LessonUnitTest : BaseTest
{
    LessonRepo repo => new (_context);
    [Fact]
    public void Add()
    {
        Lesson lesson = new Lesson() { Name = "bar", Unit = 2 };
        int result = repo.Add(lesson);
        Assert.Equal(1, result);
        Assert.Equal(6, repo.GetAll().Count());
    }
    [Fact]
    public void Remove()
    {
        Lesson lesson = repo.GetWhere(x => x.Name.Equals("Electric")).First();
        int result = repo.Remove(lesson);
        int count = repo.GetAll().Count();
        Assert.Equal(1, result);
        Assert.Equal(4, count);
    }
    [Fact]
    public void GetNotPresentedLessons()
    {
        var lesson = new Lesson() { Name = "Math", Unit = 3 };
        repo.Add(lesson);
        var lessons = repo.GetNotPresentedLessons();
        var lessonsCount = lessons.ToList().Count;
        Assert.Equal(1, lessonsCount);
    }
    [Fact]
    public void GetNotPresentedLessonsQuery()
    {
        string query = repo.GetNotPresentedLessonsQuery();
        Assert.NotNull(query);
        Assert.NotEmpty(query);
    }
}