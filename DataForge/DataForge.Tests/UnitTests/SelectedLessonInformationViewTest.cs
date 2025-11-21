namespace DataForge.Tests.UnitTests;

public class SelectedLessonInformationViewTest : BaseTest
{
    SelectedLessonInformationViewRepo Repo => new(_context);

    [Fact]
    public void GetAll()
    {
        var result = Repo.GetAll();
        var count = result.Count();
        Assert.Equal(5000, count);
    }

    [Fact]
    public void GetFirst()
    {
        var result = Repo.GetFirst();
        Assert.NotNull(result);
    }
}
