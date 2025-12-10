namespace MelliMaharat.Dal.Repos;

public class SelectionRepo : TemporalRepo<Selection>
{
    public SelectionRepo() : base() { }
    public SelectionRepo(ApplicationDbContext context) : base(context) { }
    public override IEnumerable<Selection> GetAll()
    {
        return _context.Selections.Include(x => x.Student).ThenInclude(x => x.User).ThenInclude(x => x.PersonInformation)
                                  .Include(x => x.Presentation).ThenInclude(x => x.Master).ThenInclude(x => x.User).ThenInclude(x => x.PersonInformation)
                                  .Include(x => x.Presentation).ThenInclude(x => x.Lesson)
                                  .Include(x => x.Term);
    }
}