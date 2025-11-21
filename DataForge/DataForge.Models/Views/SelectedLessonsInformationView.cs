namespace DataForge.Models.Views;

[Keyless, EntityTypeConfiguration(typeof(SelectedLessonInformationViewConfiguration))]
public class SelectedLessonsInformationView : INonPersisted
{
    public string StudentName { get; set; }
    public string LessonName { get; set; }
    public string MasterName { get; set; }
    public TimeOnly LessonStartTime { get; set; }
    public TimeOnly LessonEndTime { get; set; }
    public int LessonUnit { get; set; }
    public DateOnly EducationYear { get; set; }
    public decimal Score {  get; set; }
}