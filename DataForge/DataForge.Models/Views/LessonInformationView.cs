namespace DataForge.Models.Views;

[Keyless]
[EntityTypeConfiguration(typeof(LessonInformationViewConfiguration))]
public class LessonInformationView : INonPersisted
{
    public string Lesson {  get; set; }

    public string Master { get; set; }

    public string DayHold { get; set; } 
    
    public TimeOnly StartTime { get; set; }
    
    public TimeOnly EndTime { get; set; }
}