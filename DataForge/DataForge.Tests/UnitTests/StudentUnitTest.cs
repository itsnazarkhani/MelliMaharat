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

    [Fact]
    public void IsStudentExist()
    {
        string username = repo.GetFirst().PersonInformation.Username;
        bool result = repo.IsUserExist(username);
        Assert.True(result);

        result = repo.IsUserExist(string.Empty);
        Assert.False(result);
    }

    [Fact]
    public void IsStudentAdmin()
    {
        Person personInfo = repo.GetFirst().PersonInformation;
        bool result = repo.IsAdmin(personInfo.Username, personInfo.Password);
        Assert.False(result);

        result = repo.IsAdmin("admin", "admin");
        Assert.True(result);
    }
}