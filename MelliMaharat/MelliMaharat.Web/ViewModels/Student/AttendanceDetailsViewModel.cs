namespace MelliMaharat.Web.ViewModels.Student
{
    public class AttendanceDetailsViewModel
    {
        public Guid SelectionId { get; set; }
        public string LessonName { get; set; }
        public string MasterName { get; set; }
        public string DayHold { get; set; }
        public string TermName { get; set; }
        public List<SessionAttendanceViewModel> Sessions { get; set; } = new();
        public int TotalSessions { get; set; }
        public int PresentSessions { get; set; }

        public decimal AttendancePercentage =>
            TotalSessions > 0 ? Math.Round((decimal)PresentSessions / TotalSessions * 100, 2) : 0;
    }
}
