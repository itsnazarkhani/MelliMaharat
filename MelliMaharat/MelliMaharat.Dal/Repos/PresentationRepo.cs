
namespace MelliMaharat.Dal.Repos;

public class PresentationRepo : TemporalRepo<Presentation>
{
    public PresentationRepo() : base() { }
    public PresentationRepo(ApplicationDbContext context) : base(context) { }
    public override IEnumerable<Presentation> GetAll()
    {
        return _context.Presentations.Include(x => x.Master).ThenInclude(x => x.User).ThenInclude(x => x.PersonInformation).Include(x => x.Lesson);
    }
}