using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MelliMaharat.Models
{
    public class SelectionTime : BaseEntity
    {
        [Required(ErrorMessage = "لطفاً زمان شروع انتخاب واحد را وارد کنید.")]
        public DateTime SelectionStart { get; set; }

        [Required(ErrorMessage = "لطفاً زمان پایان انتخاب واحد را وارد کنید.")]
        public DateTime SelectionEnd { get; set; }

        [Required(ErrorMessage = "لطفاً ترم مربوطه را انتخاب کنید.")]
        [ForeignKey(nameof(TermId))]
        public Term Term { get; set; }

        [Required(ErrorMessage = "شناسه ترم الزامی است.")]
        public Guid TermId { get; set; }
    }
}
