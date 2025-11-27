using System;
using System.Collections.Generic;
using System.Text;

namespace MelliMaharat.Models
{
    public class Attendance : BaseEntity
    {
        public Selection Selection { get; set; }
        public Guid SelectionId { get; set; }

        public DateTime DateAttended { get; set; }
        public bool HasAttended { get; set; }
    }
}
