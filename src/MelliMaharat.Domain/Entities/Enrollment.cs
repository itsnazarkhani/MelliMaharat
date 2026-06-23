using MelliMaharat.Domain.Common;
using MelliMaharat.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace MelliMaharat.Domain.Entities
{
    public class Enrollment : BaseEntity
    {
        #region Properties
        public EnrollmentStatus Status { get; set; }
        public DateTime EnrolledAt { get; set; }
        #endregion

        #region Relationships
        public Guid StudentId { get; set; }
        public Student Student { get; set; } = null!;

        public Guid CourseOfferingId { get; set; }
        public CourseOffering CourseOffering { get; set; } = null!;

        public ICollection<Grade> Grades { get; set; } = [];
        #endregion
    }
}
