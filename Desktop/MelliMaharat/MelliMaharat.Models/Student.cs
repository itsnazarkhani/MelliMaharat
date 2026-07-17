namespace MelliMaharat.Models;

public class Student : BaseEntity
{
    [InverseProperty(nameof(Selection.Student))]
    public IEnumerable<Selection> Selections { get; set; } = new List<Selection>();

    [ForeignKey(nameof(UserId))]
    public User User { get; set; }
    [Required]
    public Guid UserId { get; set; }
}