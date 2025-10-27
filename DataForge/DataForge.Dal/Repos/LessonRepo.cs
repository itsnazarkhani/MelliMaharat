namespace DataForge.Dal.Repos
{
    public class LessonRepo : TemporalRepo<Lesson>
    {
        public LessonRepo() : base() { }
        public LessonRepo(ApplicationDbContext context) : base(context) { }
        public IEnumerable<Lesson> GetNotPresentedLessons()
        {
            return _table.Include(x => x.Presentations).Where(x => !x.Presentations.Any());
        }
        public string GetNotPresentedLessonsQuery() =>
            _table
                .Include(x => x.Presentations)
                .Where(x => !x.Presentations.Any())
                .Select(x => new { x.Name, x.Unit })
                .ToQueryString();
    }
}
