using MelliMaharat.Models;
using System.ComponentModel.DataAnnotations;

namespace MelliMaharat.Web.ViewModels.Admin
{
    public class SelectionEventViewModel
    {
        [Required(ErrorMessage = "زمان شروع الزامی است")]
        public DateTime SelectionStart { get; set; }

        [Required(ErrorMessage = "زمان پایان الزامی است")]
        public DateTime? SelectionEnd { get; set; }

        [Required(ErrorMessage = "لطفاً ترم را انتخاب کنید")]
        public Guid TermId { get; set; }

        public List<Term> Terms { get; set; } = new List<Term>();
    }
}
