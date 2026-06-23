using MelliMaharat.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace MelliMaharat.Domain.Entities
{
    public class Course : BaseEntity
    {
        #region Properties
        public string Code { get; set; } = null!;
        public string Title { get; set; } = null!;
        public int Credits { get; set; }
        #endregion

        #region Relationships
        public Guid DepartmentId { get; set; }
        public Department Department { get; set; } = null!;

        public ICollection<CourseOffering> Offerings { get; set; } = [];
        #endregion
    }
}
