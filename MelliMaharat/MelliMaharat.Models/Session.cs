using System;
using System.Collections.Generic;
using System.Text;

namespace MelliMaharat.Models
{
    public class Session : BaseEntity
    {
        public DateTime SessionDate { get; set; }

        [ForeignKey(nameof(SelectionId))]
        public Selection Selection { get; set; }
        public Guid SelectionId { get; set; }

        [InverseProperty(nameof(Attendance.Session))]
        public Attendance Attendance { get; set; }
    }
}
