namespace DataForge.Dal.Repos;

public class StudentRepo : Repo<Student>
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
        var studentQuery = _table.Where(x => x.Id == id).Include(x => x.Selections);
        var studentQueryString = studentQuery.ToQueryString();
        var student = studentQuery.SingleOrDefault();

        var selectionsCount = student.Selections.Count();
 
        if (selectionsCount == 0)
            throw new ArgumentException($"Selections of this Entity is empty!");

        var grades = student.Selections.Select(x => (decimal)x.Score);
        var gradesSum = grades.Sum();
        var gradesCount = grades.Count();
        var result = gradesSum / gradesCount;
        return result;
    }
}