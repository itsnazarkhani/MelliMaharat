using MelliMaharat.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace MelliMaharat.Domain.Entities
{
    public class Instructor : BaseEntity
    {
        #region Properties
        public string EmployeeNumber { get; set; } = null!;
        #endregion

        #region Relationships
        public string UserId { get; set; } = null!;

        public Guid DepartmentId { get; set; }
        public Department Department { get; set; } = null!;
        #endregion
    }
}
