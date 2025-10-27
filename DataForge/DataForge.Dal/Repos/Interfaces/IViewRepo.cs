namespace DataForge.Dal.Repos.Interfaces
{
    public interface IViewRepo<T> where T : class
    {
        IEnumerable<T> GetAll();
        //T GetFirst(int id);
        T GetFirst();
        //T GetSingle(int id);
        T GetSingle();
        IEnumerable<T> GetWhere(Func<T, bool> predicate);
        int ExecuteSqlString(string sql);
        //IEnumerable<T> GetBySqlQuery(string sql);
    }
}
