using MelliMaharat.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MelliMaharat.Models
{
    public class Term : BaseEntity
    {
        [Required(ErrorMessage = "لطفاً سال ترم را وارد کنید.")]
        public int Year { get; set; }

        [Required(ErrorMessage = "لطفاً نوع ترم را انتخاب کنید.")]
        public TermType Type { get; set; }

        [Required(ErrorMessage = "لطفاً تاریخ شروع ترم را وارد کنید.")]
        public DateOnly StartTime { get; set; }

        [Required(ErrorMessage = "لطفاً تاریخ پایان ترم را وارد کنید.")]
        public DateOnly EndTime { get; set; }

        [InverseProperty(nameof(Selection.Term))]
        public IEnumerable<Selection> Selections { get; set; } = new List<Selection>();

        [InverseProperty(nameof(SelectionTime.Term))]
        public IEnumerable<SelectionTime> SelectionTimes { get; set; } = new List<SelectionTime>();
    }
}
