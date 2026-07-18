namespace MelliMaharat.Dal.Repos;

public class LessonRepo : TemporalRepo<Lesson>
{
    public LessonRepo() : base() { }
    public LessonRepo(ApplicationDbContext context) : base(context) { }
    
    public IEnumerable<Lesson> GetNotPresentedLessons() =>
        _table.Include(x => x.Presentations).Where(x => !x.Presentations.Any());

    /// <summary>
    /// Get All Lessons Presented By specific Master
    /// </summary>
    /// <param name="master"></param>
    /// <returns></returns>
    public IEnumerable<Lesson> GetAll(Master master)
    {
        //var query = _context.Lessons.Include(x => x.Presentations.Where(x => x.Master == master)).ThenInclude(x => x.Master).ThenInclude(x => x.User).ThenInclude(x => x.PersonInformation).Distinct().ToQueryString();
        //return _context.Lessons.Include(x => x.Presentations).ThenInclude(x => x.Master).ThenInclude(x => x.User).ThenInclude(x => x.PersonInformation).Where(x => x.).Distinct();
        return _context.Masters.Where(x => x.User.Username == master.User.Username)
                               .Include(x => x.Presentations).ThenInclude(x => x.Lesson)
                               .SingleOrDefault()
                               .Presentations
                               .Select(x => x.Lesson);
    }
    //public override IEnumerable<Lesson> GetAll()
    //{
        //return _context.Lessons.Include();
    //}
    public string GetNotPresentedLessonsQuery() =>
        _table
            .Include(x => x.Presentations)
            .Where(x => !x.Presentations.Any())
            .Select(x => new { x.Name, x.Unit })
            .ToQueryString();
}