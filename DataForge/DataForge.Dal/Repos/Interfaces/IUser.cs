namespace DataForge.Dal.Repos.Interfaces;

public interface IUser
{
    bool IsUserExist(string username);
    bool IsPasswordMatch(string username, string password);
    bool IsAdmin(string username, string password);
}