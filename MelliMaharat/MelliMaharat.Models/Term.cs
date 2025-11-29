using MelliMaharat.Models.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace MelliMaharat.Models
{
    public class Term : BaseEntity
    {
        public int Year { get; set; }
        public TermType Type{ get; set; }
        public DateOnly StartTime { get; set; }
        public DateOnly EndTime { get; set; }

        [InverseProperty(nameof(Selection.Term))]
        public IEnumerable<Selection> Selections { get; set; } = new List<Selection>();
    }
}
