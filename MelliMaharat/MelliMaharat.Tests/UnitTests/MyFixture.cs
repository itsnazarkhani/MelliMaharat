namespace MelliMaharat.Tests.UnitTests;

public class MyFixture
{
    public MyFixture()
    {   
        ApplicationDbContext.FactoryDelete();
        ApplicationDbContext.FactoryMigrate();
    }
}