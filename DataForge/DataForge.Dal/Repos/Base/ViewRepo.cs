namespace DataForge.Dal.Repos.Base
{
    public class ViewRepo<T> : BaseRepo<T> ,IViewRepo<T> where T : class 
    {
        public ViewRepo() : base() { }
        public ViewRepo(ApplicationDbContext context) : base(context) { }

        public virtual int ExecuteSqlString(string sql) =>
            _context.Database.ExecuteSqlRaw(sql);

        public virtual T GetFirst() =>
            _table.First();

        //public virtual T GetFirst(int id) =>
        //    _table.Where(x => x.Id.Equals(id)).First();

        public virtual T GetSingle() =>
            _table.Single();

        //public virtual T GetSingle(int id) =>
        //    _table.Where(x => x.Id.Equals(id)).Single();

        public virtual IEnumerable<T> GetAll() =>
            _table.AsQueryable();

        public virtual IEnumerable<T> GetWhere(Func<T, bool> predicate) =>
            _table.Where(predicate);

        public virtual IEnumerable<T> GetBySqlQuery(string sql) =>
            _table.FromSqlRaw(sql);
    }
}
