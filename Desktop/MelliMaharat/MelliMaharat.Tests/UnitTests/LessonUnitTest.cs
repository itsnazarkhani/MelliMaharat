namespace MelliMaharat.Tests.UnitTests;

public class LessonUnitTest : BaseTest
{
    LessonRepo Repo => new(_context);
    [Fact]
    public void Add()
    {
        var lesson = new Lesson() { Name = "bar", Unit = 2 };
        var (result, message)= Repo.Add(lesson);
        Assert.Equal(1, result);
        Assert.Equal(31, Repo.GetAll().Count());
    }
    [Fact]
    public void Remove()
    {
        var lesson = Repo.GetWhere(x => x.Name.Equals("Electromagnetics I")).First();
        var (result, message) = Repo.Remove(lesson);
        var count = Repo.GetAll().Count();
        Assert.Equal(1, result);
        Assert.Equal(29, count);
    }
    [Fact]
    public void GetNotPresentedLessons()
    {
        var lesson = new Lesson() { Name = "Math", Unit = 3 };
        Repo.Add(lesson);
        var lessons = Repo.GetNotPresentedLessons();
        Assert.NotEmpty(lessons);
    }
    [Fact]
    public void GetNotPresentedLessonsQuery()
    {
        var query = Repo.GetNotPresentedLessonsQuery();
        Assert.NotNull(query);
        Assert.NotEmpty(query);
    }
}