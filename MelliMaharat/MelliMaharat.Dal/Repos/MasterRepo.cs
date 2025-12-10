
namespace MelliMaharat.Dal.Repos;

public class MasterRepo : Repo<Master>
{
    public MasterRepo() : base() { }
    public MasterRepo(ApplicationDbContext context) : base(context) { }

    /// <returns>all masters including User and User.PersonInformation and Department</returns>
    public override IEnumerable<Master> GetAll()
    {
        return _context.Masters.Include(x => x.Department)
                               .Include(x => x.User).ThenInclude(x => x.PersonInformation);
    }
    
    /// <returns>a master including User and User.PersonInformation</returns>
    public Master GetSingle(Master master)
    {
        return _context
                   .Masters.Where(x => x.Id == master.Id)
                   .Include(x => x.User).ThenInclude(x => x.PersonInformation)
                   .SingleOrDefault();
    }
        
}