namespace DataForge.Tests.UnitTests;

public class MasterUnitTest : BaseTest
{
    MasterRepo _repo => new MasterRepo(_context);

    [Fact]
    public void Get()
    {
        var masters = _repo.GetAll().ToList();
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
        int result = _repo.Add(master);
        Assert.Equal(1, result);
        var mastersCount = _repo.GetAll().ToList().Count();
        Assert.Equal(12, mastersCount);
        _repo.Remove(master);
        mastersCount = _repo.GetAll().ToList().Count();
        Assert.Equal(11, mastersCount);
    }

    [Fact]
    public void GetList()
    {
        int actualCount = _repo.GetAll().Count();
        Assert.Equal(11, actualCount);
    }

    [Fact]
    public void Remove()
    {
        var master = _repo.GetFirst();
        int result = _repo.Remove(master);
        Assert.Equal(1, result);
    }


    [Fact]
    public void IsMasterExist()
    {
        var result = _repo.IsUserExist(_repo.GetFirst().PersonInformation.Username);
        Assert.True(result);
    }

    [Fact]
    public void IsMasterAdmin()
    {
        var userInfo = _repo.GetFirst().PersonInformation;
        bool result = _repo.IsAdmin(userInfo.Username, userInfo.Password);
        Assert.False(result);
    }
}