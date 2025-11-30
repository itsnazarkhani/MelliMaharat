namespace MelliMaharat.Models;

[EntityTypeConfiguration(typeof(LessonConfiguration))]
public class Lesson : BaseEntity
{
    [Required, StringLength(50)]
    public string Name { get; set; }
    
    [Required]
    [Range(1, 10, ErrorMessage = "تعداد واحد باید بین 1 تا 10 باشد")]
    public int Unit { get; set; }

    [Required]
    public int Code { get; set; }

    [InverseProperty(nameof(Presentation.Lesson))]
    public IEnumerable<Presentation> Presentations { get; set; } = new List<Presentation>();
}