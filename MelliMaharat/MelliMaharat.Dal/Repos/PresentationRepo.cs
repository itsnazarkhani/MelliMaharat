
namespace MelliMaharat.Dal.Repos;

public class PresentationRepo : TemporalRepo<Presentation>
{
    public PresentationRepo() : base() { }
    public PresentationRepo(ApplicationDbContext context) : base(context) { }
    public override IEnumerable<Presentation> GetAll()
    {
        return _context.Presentations.Include(x => x.Master).ThenInclude(x => x.User).ThenInclude(x => x.PersonInformation).Include(x => x.Lesson);
    }

    public (int result, string message) Add(Presentation presentation, string masterNationalCode, string lessonName)
    {
        try
        {
            var masterQuery = _context.Masters
                              .Include(x => x.User)
                                  .ThenInclude(x => x.PersonInformation)
                              .Where(x => x.User.PersonInformation.NationalCode == masterNationalCode);

            var lessonQuery = _context.Lessons
                                      .Where(x => x.Name == lessonName);

            var presentationQuery = _context.Presentations.Where(x => x == presentation);

            if (!masterQuery.Any())
                return (-1, "This Master Does Not Exist!\nGo Add It.");
            if (!lessonQuery.Any())
                return (-2, "This Lesson Does Not Exist!\nGo Add It.");
            if (!presentationQuery.Any())
                return (-3, "This Presentation Does Not Exist!\nGo Add It.");

            presentation.Master = masterQuery.SingleOrDefault();
            presentation.Lesson = lessonQuery.FirstOrDefault();

            _context.Presentations.Add(presentation);

            return (_context.SaveChanges(), string.Empty);
        }
        catch (Exception x) 
        {
            return (0, x.Message + "\n\n" + x.InnerException.Message);
        }
    }
    public IEnumerable<Presentation> GetAll(Master master)
    {
        return _context
                   .Masters.Where(x => x.Id == master.Id)
                   .Include(x => x.Presentations)
                       .ThenInclude(x => x.Lesson)
                   .Include(x => x.User)
                       .ThenInclude(x => x.PersonInformation)
                   .SingleOrDefault()
                   .Presentations;
    }
    /// <returns>presentations including Lesson, Master, Master.User, Master.User.PersonInformation for specific Student</returns>
    public IEnumerable<Presentation> GetAll(Student student)
    {
        List<Presentation> presentations = [];
        var selections = _context.Students
                        .Where(x => x.Id == student.Id)
                        .Include(x => x.Selections).ThenInclude(x => x.Presentation).ThenInclude(x => x.Lesson)
                        .Include(x => x.Selections).ThenInclude(x => x.Presentation).ThenInclude(x => x.Master).ThenInclude(x => x.User).ThenInclude(x => x.PersonInformation)
                        .SingleOrDefault().Selections;
        foreach (var selection in selections)
            presentations.Add(selection.Presentation);
        return presentations;
    }
}