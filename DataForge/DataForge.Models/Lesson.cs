namespace DataForge.Models;

[EntityTypeConfiguration(typeof(LessonConfiguration))]
public class Lesson : BaseEntity
{
    [Required]
    public string Name { get; set; }
    
    [Required]
    public int Unit { get; set; }

    [InverseProperty(nameof(Presentation.LessonNavigation))]
    public IEnumerable<Presentation> Presentations { get; set; } = new List<Presentation>();
}