namespace DataForge.Dal.Repos.Interfaces;

public interface IRepo<T> : IViewRepo<T> where T : BaseEntity
{
    int Add(T entity);
    int AddRange(IEnumerable<T> entities);
    int Remove(T entity);
    int Remove(int id);
    int RemoveAll();
    int RemoveRange(IEnumerable<T> entities);
    int Update(T entity);
    int UpdateRange(IEnumerable<T> entities);
    T GetById(int id);
}