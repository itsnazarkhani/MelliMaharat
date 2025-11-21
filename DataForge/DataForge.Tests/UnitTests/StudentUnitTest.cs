namespace DataForge.Tests.UnitTests;

public class StudentUnitTest : BaseTest
{
    StudentRepo _repo = new StudentRepo();

    [Fact]
    public void StudentAvgSum()
    {
        decimal result = _repo.GetAverageGrade(2);
        Assert.True(result >= 0);
    }

    [Fact]
    public void IsStudentExist()
    {
        string username = _repo.GetFirst().PersonInformation.Username;
        bool result = _repo.IsUserExist(username);
        Assert.True(result);

        result = _repo.IsUserExist(string.Empty);
        Assert.False(result);
    }

    [Fact]
    public void IsStudentAdmin()
    {
        Person personInfo = _repo.GetFirst().PersonInformation;
        bool result = _repo.IsAdmin(personInfo.Username, personInfo.Password);
        Assert.False(result);

        result = _repo.IsAdmin("admin", "admin");
        Assert.True(result);
    }
}