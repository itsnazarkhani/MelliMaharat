namespace DataForge.Dal.Repos;

public class PresentationRepo : TemporalRepo<Presentation>
{
    public PresentationRepo() : base() { }
    public PresentationRepo(ApplicationDbContext context) : base(context) { }
}