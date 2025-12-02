using MelliMaharat.Models;

namespace MelliMaharat.Web.ViewModels.Student
{
    public class StudentGradesViewModel
    {
        public List<Selection> AllSelections { get; set; } = new();
        public List<TermGradesViewModel> TermGrades { get; set; } = new();
        public decimal OverallGPA { get; set; }
    }
}
