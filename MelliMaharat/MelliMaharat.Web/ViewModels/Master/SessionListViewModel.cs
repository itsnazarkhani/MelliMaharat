namespace MelliMaharat.Web.ViewModels.Master
{
    public class SessionListViewModel
    {
        public Guid SessionId { get; set; }
        public DateTime SessionDate { get; set; }
        public bool HasAttendance { get; set; }
        public int StudentCount { get; set; }
        public Guid PresentationId { get; set; }
    }
}
