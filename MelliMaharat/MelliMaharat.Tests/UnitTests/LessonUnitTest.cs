namespace MelliMaharat.Tests.UnitTests;

public class LessonUnitTest : BaseTest
{
    LessonRepo Repo => new(_context);
    [Fact]
    public void Add()
    {
        Lesson lesson = new Lesson() { Name = "bar", Unit = 2 };
        int result = Repo.Add(lesson);
        Assert.Equal(1, result);
        Assert.Equal(31, Repo.GetAll().Count());
    }
    [Fact]
    public void Remove()
    {
        Lesson lesson = Repo.GetWhere(x => x.Name.Equals("Electromagnetics I")).First();
        int result = Repo.Remove(lesson);
        int count = Repo.GetAll().Count();
        Assert.Equal(1, result);
        Assert.Equal(29, count);
    }
    [Fact]
    public void GetNotPresentedLessons()
    {
        var lesson = new Lesson() { Name = "Math", Unit = 3 };
        Repo.Add(lesson);
        var lessons = Repo.GetNotPresentedLessons();
        var lessonsCount = lessons.ToList().Count;
        Assert.Equal(7, lessonsCount);
    }
    [Fact]
    public void GetNotPresentedLessonsQuery()
    {
        string query = Repo.GetNotPresentedLessonsQuery();
        Assert.NotNull(query);
        Assert.NotEmpty(query);
    }
}