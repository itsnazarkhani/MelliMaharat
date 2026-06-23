using MelliMaharat.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace MelliMaharat.Domain.Entities
{
    public class CourseSession : BaseEntity
    {
        #region Properties
        public string? Topic { get; set; }
        public DateTime Date { get; set; }
        #endregion

        #region Relationships
        public Guid CourseOfferingId { get; set; }
        public CourseOffering CourseOffering { get; set; } = null!;

        public ICollection<Attendance> Attendances { get; set; } = [];
        #endregion
    }
}
