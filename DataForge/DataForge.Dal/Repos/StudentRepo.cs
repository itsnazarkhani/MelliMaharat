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
        var student = _table.Where(x => x.Id == id).Include(x => x.Selections).First();

        var selectionsCount = student.Selections.Count();
 
        if (selectionsCount == 0)
            throw new ArgumentException($"Selections of this Entity is empty!");

        var grades = student.Selections.Select(x => (decimal)x.Score);
        var gradesSum = grades.Sum();
        var gradesCount = grades.Count();

        return gradesSum / gradesCount;
    }
}