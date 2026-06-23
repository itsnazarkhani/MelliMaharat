using MelliMaharat.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace MelliMaharat.Domain.Entities
{
    public class Department : BaseEntity
    {
        #region Properties
        public string Name { get; set; } = null!;
        public string Code { get; set; } = null!;
        #endregion

        #region Relationships
        public ICollection<Student> Students { get; set; } = [];

        public ICollection<Instructor> Instructors { get; set; } = [];

        public ICollection<Course> Courses { get; set; } = [];
        #endregion
    }
}
