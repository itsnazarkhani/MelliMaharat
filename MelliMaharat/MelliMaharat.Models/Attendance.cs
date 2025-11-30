using System;
using System.Collections.Generic;
using System.Text;

namespace MelliMaharat.Models
{
    public class Attendance : BaseEntity
    {
        public bool HasAttended { get; set; }

        [ForeignKey(nameof(SessionId))]
        public Session Session { get; set; }
        public Guid SessionId { get; set; }
    }
}
