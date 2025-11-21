namespace DataForge.Models;

[EntityTypeConfiguration(typeof(LessonConfiguration))]
public class Lesson : BaseEntity
{
    [Required, StringLength(50)]
    public string Name { get; set; }
    
    [Required, StringLength(50)]
    public int Unit { get; set; }

    [InverseProperty(nameof(Presentation.LessonNavigation))]
    public IEnumerable<Presentation> Presentations { get; set; } = new List<Presentation>();
}