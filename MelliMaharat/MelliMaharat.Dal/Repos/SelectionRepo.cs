namespace MelliMaharat.Dal.Repos;

public class SelectionRepo : TemporalRepo<Selection>
{
    public SelectionRepo() : base() { }
    public SelectionRepo(ApplicationDbContext context) : base(context) { }
}