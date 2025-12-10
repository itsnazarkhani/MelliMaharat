
namespace MelliMaharat.Dal.Repos;

public class StudentRepo : Repo<Student>
{
    public StudentRepo() : base() { }
    public StudentRepo(ApplicationDbContext context) : base(context) { }

    public override IEnumerable<Student> GetAll() => _context.Students.Include(x => x.User).ThenInclude(x => x.PersonInformation);
}