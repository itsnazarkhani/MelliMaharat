using Microsoft.AspNetCore.Mvc.Rendering;

namespace MelliMaharat.Web.ViewModels.Admin
{
    public class AddPresentationViewModel
    {
        public Guid LessonId { get; set; }
        public Guid MasterId { get; set; }
        public int DayHold { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public DateTime ExamDate { get; set; }
        public TimeSpan ExamStartTime { get; set; }

        public List<SelectListItem> Lessons { get; set; } = new();
        public List<SelectListItem> Masters { get; set; } = new();
        public List<SelectListItem> PersianWeekDays { get; set; } = new();
    }

}
