using MelliMaharat.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace MelliMaharat.Domain.Entities
{
    public class Grade : BaseEntity
    {
        #region Properites
        public decimal Score { get; set; }
        #endregion

        #region Relationships
        public Guid EnrollmentId { get; set; }
        public Enrollment Enrollment { get; set; } = null!;

        public Guid AssessmentId { get; set; }
        public Assessment Assessment { get; set; } = null!;
        #endregion
    }
}
