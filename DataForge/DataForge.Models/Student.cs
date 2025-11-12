namespace DataForge.Models;

[EntityTypeConfiguration(typeof(StudentConfiguration))]
public class Student : BaseEntity
{
    public Person PersonInformation { get; set; } = new Person();

    [InverseProperty(nameof(Selection.StudentNavigation))]
    public IEnumerable<Selection> Selections { get; set; } = new List<Selection>();
}
