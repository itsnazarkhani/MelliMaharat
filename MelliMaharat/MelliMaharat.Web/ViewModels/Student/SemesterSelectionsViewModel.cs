using MelliMaharat.Models;
using System;
using System.Collections.Generic;

namespace MelliMaharat.Web.ViewModels.Student
{
    /// <summary>
    /// ViewModel for displaying selected lessons of a specific semester
    /// </summary>
    public class SemesterSelectionsViewModel
    {
        public Guid TermId { get; set; }
        public string TermName { get; set; }
        public List<Selection> Selections { get; set; } = new();
        public int TotalUnits { get; set; }
        public int TotalLessons { get; set; }
    }
}