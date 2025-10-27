namespace DataForge.Models.Views
{
    [Keyless]
    [EntityTypeConfiguration(typeof(LessonInformationViewConfiguration))]
    public class LessonInformationView : INonPersisted
    {
        [StringLength(50)]
        public string Lesson {  get; set; }

        [StringLength(50)]
        public string Master { get; set; }

        [StringLength(50)]
        public string DayHold { get; set; } 
        
        public TimeOnly StartTime { get; set; }
        
        public TimeOnly EndTime { get; set; }
    }
}
