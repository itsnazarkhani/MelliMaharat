using MelliMaharat.Web.ViewModels.Master;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace MelliMaharat.Web.ViewModels.Master
{
    public class GradeItemViewModel
    {
        public Guid SelectionId { get; set; }

        [Required(ErrorMessage = "نام دانشجو الزامی است")]
        public string StudentName { get; set; }

        [Range(0, 20, ErrorMessage = "نمره باید بین 0 تا 20 باشد")]
        public decimal CurrentScore { get; set; }

        [Range(0, 20, ErrorMessage = "نمره باید بین 0 تا 20 باشد")]
        public decimal NewScore { get; set; }

        public bool IsModified { get; set; }
    }

    public class SubmitGradesViewModel
    {
        public Guid PresentationId { get; set; }

        [Required(ErrorMessage = "درس الزامی است")]
        public string LessonName { get; set; }

        [Required]
        public List<GradeItemViewModel> Items { get; set; } = new List<GradeItemViewModel>();

        public DateTime SubmissionDate { get; set; } = DateTime.Now;

        // Stats
        public int TotalStudents { get; set; }
        public int GradesSubmitted { get; set; }
        public decimal AverageGrade { get; set; }
    }

    public class GradesSummaryViewModel
    {
        public Guid PresentationId { get; set; }

        public string LessonName { get; set; }

        public List<GradeSummaryItemViewModel> Items { get; set; } = new List<GradeSummaryItemViewModel>();

        public DateTime? SubmissionDate { get; set; }

        public bool IsSubmitted { get; set; }
    }

    public class GradeSummaryItemViewModel
    {
        public Guid SelectionId { get; set; }

        public string StudentName { get; set; }

        public decimal Grade { get; set; }

        public bool IsGraded { get; set; }
    }

    public class PresentationGradesStatusViewModel
    {
        public Guid PresentationId { get; set; }

        public string LessonName { get; set; }

        public string DayHold { get; set; }

        public int TotalStudents { get; set; }

        public int GradedStudents { get; set; }

        public decimal PercentageComplete => TotalStudents > 0
            ? Math.Round((GradedStudents * 100m) / TotalStudents, 2)
            : 0;

        public bool IsFullyGraded => GradedStudents == TotalStudents;

        public DateTime? LastModified { get; set; }
    }
}