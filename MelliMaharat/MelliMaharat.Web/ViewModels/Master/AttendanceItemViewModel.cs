using System.ComponentModel.DataAnnotations;

namespace MelliMaharat.Web.ViewModels.Master
{
    public class AttendanceItemViewModel
    {
        public Guid SelectionId { get; set; }

        [Required(ErrorMessage = "نام دانشجو الزامی است")]
        public string StudentName { get; set; }

        public Guid AttendanceId { get; set; }

        public bool HasAttended { get; set; }
    }
}