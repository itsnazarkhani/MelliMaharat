namespace DataForge.Dal.Repos;

public class StudentRepo : Repo<Student>, IUser
{
    public StudentRepo() : base() { }
    public StudentRepo(ApplicationDbContext context) : base(context) { }


    /// <summary>
    ///  returns Average Grade of Entity Based On Id
    /// </summary>
    /// <param name="id"></param>
    /// <returns>avg Grade in decimal</returns>
    /// <exception cref="ArgumentException"></exception>
    public decimal GetAverageGrade(int id)
    {
        var student = _table
                          .Where(x => x.Id == id)
                          .Include(x => x.Selections)
                          .SingleOrDefault();
        
        //var studentQueryString = studentQuery.ToQueryString();

        var selectionsCount = student.Selections.Count();
 
        if (selectionsCount == 0)
            throw new ArgumentException($"Selections of this Entity is empty!");

        var grades = student.Selections.Select(x => x.Score);
        var gradesSum = grades.Sum();
        var gradesCount = grades.Count();
        var result = gradesSum / gradesCount;
        return result;
    }

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