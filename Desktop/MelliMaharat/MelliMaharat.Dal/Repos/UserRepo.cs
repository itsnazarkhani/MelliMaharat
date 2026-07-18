namespace MelliMaharat.Dal.Repos;

public class UserRepo : Repo<User>
{
    public User GetSingle(string username)
    {
        return _table.Where(x => x.Username == username).Include(x => x.PersonInformation).SingleOrDefault();
    }
}
