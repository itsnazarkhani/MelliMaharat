namespace MelliMaharat.Dal.Repos.Base;

//public class Repo<T> : ViewRepo<T>, IRepo<T> where T : BaseEntity, new()
public class Repo<T> : ViewRepo<T> where T : BaseEntity, new()
{
    public Repo() : base() { }
    public Repo(ApplicationDbContext context) : base(context) { }

    public virtual (int result, string message) Add(T entity)
    {
        try
        {
            if (_table.Contains(entity))
                return (-1, "Entity Already Exist!");

            _table.Add(entity);
            return (_context.SaveChanges(), string.Empty);
        }
        catch (Exception x)
        {
            return (0, x.Message + "\n\n" + x.InnerException.Message);
        }
    }

    public virtual (int result, string message) AddRange(IEnumerable<T> entities)
    {
        try
        {
            _table.AddRange(entities);
            return (_context.SaveChanges(), string.Empty);
        }
        catch (Exception x)
        {
            return (0, x.Message + "\n\n" + x.InnerException.Message);
        }
    }

    public virtual (int result, string message) Remove(T entity)
    {
        try
        {
            if (!_table.Contains(entity))
                return (-1, "Entity Does Not Exist");
            _table.Where(x => x.Id == entity.Id).SingleOrDefault().IsDeleted = true;
            return (_context.SaveChanges(), string.Empty);
        }
        catch (Exception x)
        {
            return (0, x.Message + "\n\n" + x.InnerException.Message);
        }
    }

    public virtual (int result, string message) Remove(Guid id)
    {
        try
        {
            _table.Remove(GetById(id));
            return (_context.SaveChanges(), string.Empty);
        }
        catch (Exception x)
        {
            return (0, x.Message + "\n\n" + x.InnerException.Message);
        }
    }

    public virtual (int result, string message) RemoveAll()
    {
        try
        {
            _table.RemoveRange(GetAll());
            return (_context.SaveChanges(), string.Empty);
        }
        catch (Exception x)
        {
            return (0, x.Message + "\n\n" + x.InnerException.Message);
        }
    }

    public virtual (int result, string message) RemoveRange(IEnumerable<T> entities)
    {
        try
        {
            _table.RemoveRange(entities);
            return (_context.SaveChanges(), string.Empty);
        }
        catch (Exception x)
        {
            return (0, x.Message + "\n\n" + x.InnerException.Message);
        }
    }

    public virtual (int result, string message) Update(T entity)
    {
        try
        {
            _table.Update(entity);
            return (_context.SaveChanges(), string.Empty);
        }
        catch (Exception x)
        {
            return (0, x.Message + "\n\n" + x.InnerException.Message);
        }
    }

    public virtual (int result, string message) UpdateRange(IEnumerable<T> entities)
    {
        try
        {
            _table.UpdateRange(entities);
            return (_context.SaveChanges(), string.Empty);
        }
        catch (Exception x)
        {
            return (0, x.Message + "\n\n" + x.InnerException.Message);
        }
    }

    public virtual T GetById(Guid id) => _table.Where(x => x.Id == id).Single();
}