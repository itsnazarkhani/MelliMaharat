namespace DataForge.Tests.UnitTests;

public class LessonInformationViewTest : BaseTest 
{
    LessonInformationViewRepo repo => new(_context);
    [Fact]
    public void GetAll()
    {
        var list = repo.GetAll().ToList();
        var listCount = list.Count;
        Assert.Equal(50, listCount);
    }
}