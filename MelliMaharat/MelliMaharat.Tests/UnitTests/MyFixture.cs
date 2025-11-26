namespace MelliMaharat.Tests.UnitTests;

public class MyFixture
{
    public MyFixture() => ApplicationDbContext.FactoryMigrate();
}