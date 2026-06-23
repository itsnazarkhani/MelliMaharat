using MelliMaharat.Domain.Common;
using MelliMaharat.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace MelliMaharat.Domain.Entities
{
    public class Attendance : BaseEntity
    {
        #region Properties
        public AttendanceStatus Status { get; set; }
        public string? Note { get; set; }
        #endregion

        #region Relationships
        public Guid CourseSessionId { get; set; }
        public CourseSession CourseSession { get; set; } = null!;

        public Guid StudentId { get; set; }
        public Student Student { get; set; } = null!;
        #endregion
    }
}
