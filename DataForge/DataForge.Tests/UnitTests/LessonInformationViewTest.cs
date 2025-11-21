namespace DataForge.Tests.UnitTests;

public class LessonInformationViewTest : BaseTest 
{
    LessonInformationViewRepo _repo => new(_context);
    [Fact]
    public void GetAll()
    {
        var list = _repo.GetAll().ToList();
        var listCount = list.Count;
        Assert.Equal(50, listCount);
    }
}