using Microsoft.AspNetCore.Mvc.Rendering;

namespace MelliMaharat.Web.ViewModels.Admin
{
    public class AddPresentationViewModel
    {
        public Guid LessonId { get; set; }
        public Guid MasterId { get; set; }
        public int DayHold { get; set; }
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }
        public DateOnly ExamDate { get; set; }
        public TimeOnly ExamStartTime { get; set; }

        public List<SelectListItem> Lessons { get; set; } = new();
        public List<SelectListItem> Masters { get; set; } = new();
        public List<SelectListItem> PersianWeekDays { get; set; } = new();
    }

}
