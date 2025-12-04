namespace MelliMaharat.Web.ViewModels.Master
{
    public class PresentedLessonViewModel
    {
        public Guid PresentationId { get; set; }
        public string LessonName { get; set; }
        public string DayHold { get; set; }
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }
    }
}
