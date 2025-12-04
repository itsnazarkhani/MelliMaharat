using MelliMaharat.Models;
using MelliMaharat.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace MelliMaharat.Web.ViewModels.Admin
{
    public class TermViewModel
    {
        [Required(ErrorMessage = "سال الزامی است")]
        public int Year { get; set; }

        [Required(ErrorMessage = "نوع ترم را انتخاب کنید")]
        public TermType Type { get; set; }

        [Required(ErrorMessage = "زمان شروع الزامی است")]
        public DateOnly StartTime { get; set; }

        [Required(ErrorMessage = "زمان پایان الزامی است")]
        public DateOnly EndTime { get; set; }
    }
}
