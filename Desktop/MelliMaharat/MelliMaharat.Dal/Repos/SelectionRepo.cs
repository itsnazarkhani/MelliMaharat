using Microsoft.EntityFrameworkCore.Metadata;

namespace MelliMaharat.Dal.Repos;

public class SelectionRepo : TemporalRepo<Selection>
{
    public SelectionRepo() : base() { }
    public SelectionRepo(ApplicationDbContext context) : base(context) { }
    public (int result, string message) Add(Selection selection, string studentNationalCode, Guid presentationId)
    {
        try
        {
            var studentQuery = _context.Students.Include(x => x.User).ThenInclude(x => x.PersonInformation).Where(x => x.User.PersonInformation.NationalCode == studentNationalCode);
            var presentationQuery = _context.Presentations
                                            .Include(x => x.Lesson)
                                            .Include(x => x.Master)
                                                .ThenInclude(x => x.User)
                                                    .ThenInclude(x => x.PersonInformation)
                                            .Where(x => x.Id == presentationId);

            if (!studentQuery.Any())
                return (-1, "This Student Does Not Exist!");
            if (!presentationQuery.Any())
                return (-2, "This Presentation Does Not Exist!");

            selection.Student = studentQuery.SingleOrDefault();
            selection.Presentation = presentationQuery.SingleOrDefault();

            _context.Selections.Add(selection);

            return (_context.SaveChanges(), string.Empty);
        }
        catch (Exception x)
        {
            return (0, x.Message + "\n\n" + x.InnerException.Message);
        }
    }
    public override IEnumerable<Selection> GetAll()
    {
        return _context.Selections.Include(x => x.Student).ThenInclude(x => x.User).ThenInclude(x => x.PersonInformation)
                                  .Include(x => x.Presentation).ThenInclude(x => x.Master).ThenInclude(x => x.User).ThenInclude(x => x.PersonInformation)
                                  .Include(x => x.Presentation).ThenInclude(x => x.Lesson)
                                  .Include(x => x.Term);
    }
    /// <returns>selections from specific Student including Student.User.PersonInformation, Presentation.Lesson, Term</returns>
    public IEnumerable<Selection> GetAll(Student student)
    {
        return _context.Students.Where(x => x.Id == student.Id)
                                 .Include(x => x.User).ThenInclude(x => x.PersonInformation)
                                 .Include(x => x.Selections).ThenInclude(x => x.Presentation).ThenInclude(x => x.Lesson)
                                 .Include(x => x.Selections).ThenInclude(x => x.Presentation).ThenInclude(x => x.Master).ThenInclude(x => x.User).ThenInclude(x => x.PersonInformation)
                                 .Include(x => x.Selections).ThenInclude(x => x.Term).SingleOrDefault().Selections;
    }
}