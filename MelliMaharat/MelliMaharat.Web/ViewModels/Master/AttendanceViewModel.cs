using System.ComponentModel.DataAnnotations;

namespace MelliMaharat.Web.ViewModels.Master
{
    public class AttendanceViewModel
    {
        public Guid SessionId { get; set; }

        public Guid PresentationId { get; set; }

        [Required]
        public List<AttendanceItemViewModel> Items { get; set; } = new List<AttendanceItemViewModel>();
    }
}