namespace DataForge.Tests.UnitTests;

public class PresentationUnitTest : BaseTest
{
    PresentationRepo Repo => new(_context);

    [Fact]
    public void Get()
    {
        var presentations = Repo.GetAll().ToList();
        var presentationsCount= presentations.Count();
        Assert.Equal(50, presentationsCount);
    }
    [Fact]
    public void Add()
    {
        Lesson lesson = new Lesson() { Name = "Programming", Unit = 2 };
        Person person = new Person() { Age = 25, FirstName = "Esmail", LastName = "Jahanbakhsh", Password = "1111111111", Username = "Somethingdo"};
        Master master = new Master() { Graduation = "Phd", PersonInformation = person };

        var presentation = new Presentation()
        {
            LessonNavigation = lesson,
            MasterNavigation = master,
            DayHold = "Saturday",
            StartTime = new TimeOnly(10, 30),
            EndTime = new TimeOnly(12, 00)
        };
        int result = Repo.Add(presentation);
        Assert.Equal(3, result);

        var presentations = Repo.GetAll().ToList();
        Assert.Equal(51, presentations.Count);
    }
    [Fact]
    public void Update()
    {
        var presentation = Repo.GetFirst();
        presentation.DayHold = "Sunday";
        int result = Repo.Update(presentation);
        Assert.Equal(1, result);
    }
}