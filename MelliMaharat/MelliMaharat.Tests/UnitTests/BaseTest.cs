namespace MelliMaharat.Tests.UnitTests;

public class BaseTest : IDisposable
{
    protected readonly ApplicationDbContext _context;
    readonly IDbContextTransaction _transaction;

    public BaseTest()
    {
        _context = new ApplicationDbContextFactory().CreateDbContext();
        _context.Database.OpenConnection();
        _transaction = _context.Database.BeginTransaction();
    }
    public void Dispose()
    {
        _transaction.Rollback();
        _context.Database.CloseConnection();
        _context.Dispose();
    }
}