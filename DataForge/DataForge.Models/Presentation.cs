namespace DataForge.Models;

[EntityTypeConfiguration(typeof(PresentationConfiguration))]
public class Presentation : BaseEntity
{
    [ForeignKey(nameof(MasterId))]
    public Master MasterNavigation {  get; set; }
    public int MasterId { get; set; }

    [ForeignKey(nameof(LessonId))]
    public Lesson LessonNavigation { get; set; }
    public int LessonId { get; set; }

    [Required]
    public string DayHold { get; set; }
    
    [Required]
    public TimeOnly StartTime { get; set; }
    
    [Required]
    public TimeOnly EndTime { get; set; }
}