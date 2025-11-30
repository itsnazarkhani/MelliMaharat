namespace MelliMaharat.Models;

[EntityTypeConfiguration(typeof(LessonConfiguration))]
public class Lesson : BaseEntity
{
    [Required, StringLength(50)]
    public string Name { get; set; }
    
    [Required, StringLength(50)]
    public int Unit { get; set; }

    [InverseProperty(nameof(Presentation.Lesson))]
    public IEnumerable<Presentation> Presentations { get; set; } = new List<Presentation>();
}