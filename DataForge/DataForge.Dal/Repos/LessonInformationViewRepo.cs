namespace DataForge.Dal.Repos;

public class LessonInformationViewRepo : ViewRepo<LessonInformationView> 
{
    public LessonInformationViewRepo() : base() { }
    public LessonInformationViewRepo(ApplicationDbContext context) : base(context) { }
}