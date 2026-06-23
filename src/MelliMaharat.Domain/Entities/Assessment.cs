using MelliMaharat.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace MelliMaharat.Domain.Entities
{
    public class Assessment : BaseEntity
    {
        #region Properties
        public string Title { get; set; } = null!;
        public decimal MaxScore { get; set; }
        public decimal Weight { get; set; } // percentage
        #endregion

        #region Relationships
        public Guid CourseOfferingId { get; set; }
        public CourseOffering CourseOffering { get; set; } = null!;

        public ICollection<Grade> Grades { get; set; } = [];
        #endregion
    }
}
