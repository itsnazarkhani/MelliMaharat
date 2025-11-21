namespace DataForge.Dal.Repos;

public class MasterRepo : Repo<Master>, IUser
{
    public MasterRepo() : base() { }
    public MasterRepo(ApplicationDbContext context) : base(context) { }

    public bool IsAdmin(string username, string password)
    {
        return _table.FirstOrDefault(x => x.PersonInformation.Username == username && x.PersonInformation.Password == password)
                     .PersonInformation
                     .IsAdmin;
    }

    public bool IsPasswordMatch(string username, string password)
    {
        var user = _table.FirstOrDefault(x => x.PersonInformation.Username == username);
        return user.PersonInformation.Password == password;
    }

    public bool IsUserExist(string username)
    {
        return _table.Where(x => x.PersonInformation.Username == username)
                     .Any();
    }
}