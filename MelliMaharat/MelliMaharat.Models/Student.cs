namespace MelliMaharat.Models;

public class Student : BaseEntity
{
    [InverseProperty(nameof(Selection.StudentNavigation))]
    public IEnumerable<Selection> Selections { get; set; } = new List<Selection>();

    public Guid UserId { get; set; }
    public User User { get; set; }
}