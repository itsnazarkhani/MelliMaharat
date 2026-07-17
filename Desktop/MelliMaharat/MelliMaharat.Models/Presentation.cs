namespace MelliMaharat.Models;

[EntityTypeConfiguration(typeof(PresentationConfiguration))]
public class Presentation : BaseEntity
{
    [Required, StringLength(50)]
    public string DayHold { get; set; }

    [Required]
    public TimeOnly StartTime { get; set; }

    [Required]
    public TimeOnly EndTime { get; set; }

    public DateOnly ExamDate { get; set; }
    public TimeOnly ExamStartTime { get; set; }

    [InverseProperty(nameof(Selection.Presentation))]
    public IEnumerable<Selection> Selections { get; set; } = new List<Selection>();

    [Required, ForeignKey(nameof(MasterId))]
    public Master Master { get; set; }[ForeignKey(nameof(MasterId))]

    [Required]
    public Guid MasterId { get; set; }

    [Required, ForeignKey(nameof(LessonId))]
    public Lesson Lesson { get; set; }

    [Required]
    public Guid LessonId { get; set; }
}
