
namespace MelliMaharat.Dal.Repos;

public class MasterRepo : Repo<Master>
{
    public MasterRepo() : base() { }
    public MasterRepo(ApplicationDbContext context) : base(context) { }

    public override IEnumerable<Master> GetAll()
    {
        return _context.Masters.Include(x => x.Department).Include(x => x.User).ThenInclude(x => x.PersonInformation);
    }
}