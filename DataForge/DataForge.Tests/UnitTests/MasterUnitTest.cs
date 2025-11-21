namespace DataForge.Tests.UnitTests;

public class MasterUnitTest : BaseTest
{
    MasterRepo Repo => new(_context);

    [Fact]
    public void Get()
    {
        var masters = Repo.GetAll().ToList();
        var mastersCount = masters.Count();
        Assert.Equal(11, mastersCount);
    }

    [Fact]
    public void Add()
    {
        var person = new Person()
        {
            Age = 21,
            FirstName = "Michelle",
            LastName = "Jorjany",
            Password = "1111111111",
            Username = "Something.sdsf"
        };
        var master = new Master()
        {
            Graduation = "Phd",
            PersonInformation = person
        };
        int result = Repo.Add(master);
        Assert.Equal(1, result);
        var mastersCount = Repo.GetAll().ToList().Count();
        Assert.Equal(12, mastersCount);
        Repo.Remove(master);
        mastersCount = Repo.GetAll().ToList().Count();
        Assert.Equal(11, mastersCount);
    }

    [Fact]
    public void GetList()
    {
        int actualCount = Repo.GetAll().Count();
        Assert.Equal(11, actualCount);
    }

    [Fact]
    public void Remove()
    {
        var master = Repo.GetFirst();
        int result = Repo.Remove(master);
        Assert.Equal(1, result);
    }


    [Fact]
    public void IsMasterExist()
    {
        var result = Repo.IsUserExist(Repo.GetFirst().PersonInformation.Username);
        Assert.True(result);
    }

    [Fact]
    public void IsMasterAdmin()
    {
        var userInfo = Repo.GetFirst().PersonInformation;
        bool result = Repo.IsAdmin(userInfo.Username, userInfo.Password);
        Assert.False(result);
    }
}