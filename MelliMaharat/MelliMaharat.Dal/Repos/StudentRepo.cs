
namespace MelliMaharat.Dal.Repos;

public class StudentRepo : Repo<Student>
{
    public StudentRepo() : base() { }
    public StudentRepo(ApplicationDbContext context) : base(context) { }

    public override IEnumerable<Student> GetAll() => _context.Students.Include(x => x.User).ThenInclude(x => x.PersonInformation);
    public IEnumerable<Student> GetAll(Master master)
    {
        List<Student> students = [];
        List<Selection> selections = [];

        var presentations = _context
                                .Masters.Where(x => x.Id == master.Id)
                                .Include(x => x.Presentations).ThenInclude(x => x.Selections).ThenInclude(x => x.Student).ThenInclude(x => x.User).ThenInclude(x => x.PersonInformation)
                                .SingleOrDefault().Presentations;
        
        foreach (var presentation in presentations)
            selections.AddRange(presentation.Selections);
        foreach (var selection in selections)
            students.Add(selection.Student);
        return students;
    }
}