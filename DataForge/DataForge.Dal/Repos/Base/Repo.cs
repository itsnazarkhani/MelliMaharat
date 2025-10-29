namespace DataForge.Dal.Repos.Base;

public class Repo<T> : ViewRepo<T>, IRepo<T> where T : BaseEntity, new()
{
    public Repo() : base() { }
    public Repo(ApplicationDbContext context) : base(context) { }

    public virtual int Add(T entity)
    {
        _table.Add(entity);
        return _context.SaveChanges();
    }

    public virtual int AddRange(IEnumerable<T> entities)
    {
        _table.AddRange(entities);
        return _context.SaveChanges();
    }

    public virtual int Remove(T entity)
    {
        _table.Remove(entity);
        return _context.SaveChanges();
    }

    public virtual int Remove(int id)
    {
        _table.Remove(GetById(id));
        return _context.SaveChanges();
    }

    public virtual int RemoveAll()
    {
        _table.RemoveRange(GetAll());
        return _context.SaveChanges();
    }

    public virtual int RemoveRange(IEnumerable<T> entities)
    {
        _table.RemoveRange(entities);
        return _context.SaveChanges();
    }

    public virtual int Update(T entity)
    {
        _table.Update(entity);
        return _context.SaveChanges();
    }

    public virtual int UpdateRange(IEnumerable<T> entities)
    {
        _table.UpdateRange(entities);
        return _context.SaveChanges();
    }

    public virtual T GetById(int id) =>
        _table.Where(x => x.Id == id).Single();
}