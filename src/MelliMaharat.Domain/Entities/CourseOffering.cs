using MelliMaharat.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace MelliMaharat.Domain.Entities
{
    public class CourseOffering : BaseEntity
    {
        #region Properties
        public string Semester { get; set; } = null!;
        public int Capacity { get; set; }
        #endregion

        #region RelationShips
        public Guid CourseId { get; set; }
        public Course Course { get; set; } = null!;

        public Guid InstructorId { get; set; }
        public Instructor Instructor { get; set; } = null!;

        public ICollection<Enrollment> Enrollments { get; set; } = [];
        #endregion
    }
}
