namespace DataForge.Tests.UnitTests;

public class StudentUnitTest : BaseTest
{
    StudentRepo repo = new StudentRepo();

    [Fact]
    public void StudentAvgSum()
    {
        decimal result = repo.GetAverageGrade(2);
        Assert.True(result >= 0);
    }
}