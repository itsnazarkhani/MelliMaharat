namespace DataForge.Dal.Repos.Interfaces;

public interface IViewRepo<T> where T : class
{
    IEnumerable<T> GetAll();
    T GetFirst();
    T GetSingle();
    IEnumerable<T> GetWhere(Func<T, bool> predicate);
    int ExecuteSqlString(string sql);
}