namespace DataForge.Tests.UnitTests;

public class StudentUnitTest : BaseTest
{
    StudentRepo Repo => new(_context);

    [Fact]
    public void StudentAvgSum()
    {
        decimal result = Repo.GetAverageGrade(2);
        Assert.True(result >= 0);
    }

    [Fact]
    public void IsStudentExist()
    {
        string username = Repo.GetFirst().PersonInformation.Username;
        bool result = Repo.IsUserExist(username);
        Assert.True(result);

        result = Repo.IsUserExist(string.Empty);
        Assert.False(result);
    }

    [Fact]
    public void IsStudentAdmin()
    {
        Person personInfo = Repo.GetFirst().PersonInformation;
        bool result = Repo.IsAdmin(personInfo.Username, personInfo.Password);
        Assert.False(result);

        result = Repo.IsAdmin("admin", "admin");
        Assert.True(result);
    }
}