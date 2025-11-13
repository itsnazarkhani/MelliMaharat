namespace DataForge.Tests.UnitTests;

public class MasterUnitTest : BaseTest
{
    MasterRepo masterRepo => new MasterRepo(_context);

    [Fact]
    public void Get()
    {
        var masters = masterRepo.GetAll().ToList();
        var mastersCount = masters.Count();
        Assert.Equal(10, mastersCount);
    }

    [Fact]
    public void Add()
    {
        var person = new Person()
        {
            Age = 21,
            FirstName = "Michelle",
            LastName = "Jorjany"
        };
        var master = new Master()
        {
            Graduation = "Phd",
            PersonInformation = person
        };
        int result = masterRepo.Add(master);
        Assert.Equal(1, result);
        var mastersCount = masterRepo.GetAll().ToList().Count();
        Assert.Equal(11, mastersCount);
        masterRepo.Remove(master);
        mastersCount = masterRepo.GetAll().ToList().Count();
        Assert.Equal(10, mastersCount);
    }

    [Fact]
    public void GetList()
    {
        int actualCount = masterRepo.GetAll().Count();
        Assert.Equal(10, actualCount);
    }

    [Fact]
    public void Remove()
    {
        var master = masterRepo.GetFirst();
        int result = masterRepo.Remove(master);
        Assert.Equal(1, result);
    }
}