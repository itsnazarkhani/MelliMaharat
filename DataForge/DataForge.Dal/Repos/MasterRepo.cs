namespace DataForge.Dal.Repos;

public class MasterRepo : Repo<Master>
{
    public MasterRepo() : base() { }
    public MasterRepo(ApplicationDbContext context) : base(context) { }
}