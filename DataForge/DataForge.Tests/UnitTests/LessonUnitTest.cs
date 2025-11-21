namespace DataForge.Tests.UnitTests;

public class LessonUnitTest : BaseTest
{
    LessonRepo _repo => new(_context);
    [Fact]
    public void Add()
    {
        Lesson lesson = new Lesson() { Name = "bar", Unit = 2 };
        int result = _repo.Add(lesson);
        Assert.Equal(1, result);
        Assert.Equal(6, _repo.GetAll().Count());
    }
    [Fact]
    public void Remove()
    {
        Lesson lesson = _repo.GetWhere(x => x.Name.Equals("Electric")).First();
        int result = _repo.Remove(lesson);
        int count = _repo.GetAll().Count();
        Assert.Equal(1, result);
        Assert.Equal(4, count);
    }
    [Fact]
    public void GetNotPresentedLessons()
    {
        var lesson = new Lesson() { Name = "Math", Unit = 3 };
        _repo.Add(lesson);
        var lessons = _repo.GetNotPresentedLessons();
        var lessonsCount = lessons.ToList().Count;
        Assert.Equal(1, lessonsCount);
    }
    [Fact]
    public void GetNotPresentedLessonsQuery()
    {
        string query = _repo.GetNotPresentedLessonsQuery();
        Assert.NotNull(query);
        Assert.NotEmpty(query);
    }
}