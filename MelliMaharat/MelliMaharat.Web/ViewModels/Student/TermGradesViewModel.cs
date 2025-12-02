using MelliMaharat.Models;
using MelliMaharat.Models.Enums;

namespace MelliMaharat.Web.ViewModels.Student
{
    public class TermGradesViewModel
    {
        public Guid TermId { get; set; }
        public int Year { get; set; }
        public TermType Type { get; set; }
        public List<Selection> Selections { get; set; } = new();
        public decimal GPA { get; set; }
        public int TotalUnits { get; set; }
    }
}
