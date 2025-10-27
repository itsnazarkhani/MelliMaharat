namespace DataForge.Dal.Repos.Base
{
    public class BaseRepo<T> where T : class
    {
        internal readonly ApplicationDbContext _context;
        internal readonly DbSet<T> _table;

        public BaseRepo()
        {
            _context = new ApplicationDbContextFactory().CreateDbContext();
            _table = _context.Set<T>();
        }
        public BaseRepo(ApplicationDbContext context)
        {
            _context = context;
            _table = _context.Set<T>();
        }
    }
}
