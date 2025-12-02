namespace MelliMaharat.Web.ViewModels.Student
{
    public class SessionAttendanceViewModel
    {
        public Guid SessionId { get; set; }
        public DateTime SessionDate { get; set; }
        public bool IsPresent { get; set; }
    }
}
