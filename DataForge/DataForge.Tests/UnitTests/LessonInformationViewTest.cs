namespace DataForge.Tests.UnitTests;

public class LessonInformationViewTest : BaseTest 
{
    LessonInformationViewRepo Repo => new(_context);
    [Fact]
    public void GetAll()
    {
        var list = Repo.GetAll();
        var listCount = list.Count();
        Assert.Equal(50, listCount);
    }
}