using MelliMaharat.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace MelliMaharat.Domain.Entities
{
    public class Student : BaseEntity
    {
        #region Properties
        public string StudentNumber { get; set; } = null!;
        public DateOnly? DateOfBirth { get; set; }
        #endregion

        #region Relationships
        public string UserId { get; set; } = null!;

        public Guid DepartmentId { get; set; }
        public Department Department { get; set; } = null!;

        public ICollection<Enrollment> Enrollments { get; set; } = [];
        #endregion
    }
}
