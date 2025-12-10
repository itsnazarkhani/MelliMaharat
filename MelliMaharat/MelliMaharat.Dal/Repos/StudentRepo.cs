
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
    /// <returns>single Student including User and User.PersonInformation</returns>
    public Student GetSingle(Student student)
    {
        return _context.Students.Where(x => x.Id ==  student.Id).Include(x => x.User).ThenInclude(x => x.PersonInformation).SingleOrDefault();
    }
    public decimal GetAvgGrade(Student student)
    {
        var studentEntity = _context.Students
            .Include(x => x.Selections)
                .ThenInclude(s => s.Presentation)
                    .ThenInclude(p => p.Lesson)
            .FirstOrDefault(x => x.Id == student.Id);

        var selections = studentEntity?.Selections;
        if (selections == null || !selections.Any())
            return 0m;

        var weighted = selections.Select
                                    (
                                        s => new
                                        {
                                            Grade = s.Score,
                                            Unit = s.Presentation.Lesson.Unit
                                        }
                                    );

        var totalUnits = weighted.Sum(w => w.Unit);
        return totalUnits == 0 ? 0m : weighted.Sum(w => w.Grade * w.Unit) / totalUnits;
    }
}