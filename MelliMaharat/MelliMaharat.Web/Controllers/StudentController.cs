using MelliMaharat.Dal.UnitOfWork;
using MelliMaharat.Models;
using MelliMaharat.Models.Enums;
using MelliMaharat.Web.Filters;
using MelliMaharat.Web.ViewModels;
using MelliMaharat.Web.ViewModels.Student;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MelliMaharat.Web.Controllers
{
    [AuthorizeByRole(UserRoles.Student)]
    public class StudentController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public StudentController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // Get current logged-in student ID from User claims
        private Guid GetCurrentStudentId()
        {
            var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);
            if (Guid.TryParse(userIdClaim?.Value, out var userId))
            {
                var student = _unitOfWork.Students
                    .GetAll()
                    .FirstOrDefault(s => s.UserId == userId);
                return student?.Id ?? Guid.Empty;
            }
            return Guid.Empty;
        }

        #region Selected Units
        /// <summary>
        /// Display all units selected by the current student
        /// </summary>
        public async Task<IActionResult> Selections()
        {
            var studentId = GetCurrentStudentId();
            if (studentId == Guid.Empty)
                return Unauthorized();

            var selections = await _unitOfWork.Selections
                .GetAll()
                .Where(s => s.StudentId == studentId)
                .Include(s => s.Presentation)
                .ThenInclude(p => p.Lesson)
                .Include(s => s.Presentation)
                .ThenInclude(p => p.Master)
                .ThenInclude(m => m.User)
                .ThenInclude(u => u.PersonInformation)
                .Include(s => s.Term)
                .OrderByDescending(s => s.Term.Year)
                .ThenByDescending(s => s.Term.Type)
                .ToListAsync();

            return View(selections);
        }

        /// <summary>
        /// Display selected lessons of a specific semester
        /// </summary>
        public async Task<IActionResult> SemesterSelections(Guid termId)
        {
            var studentId = GetCurrentStudentId();
            if (studentId == Guid.Empty)
                return Unauthorized();

            var selections = await _unitOfWork.Selections
                .GetAll()
                .Where(s => s.StudentId == studentId && s.TermId == termId)
                .Include(s => s.Presentation)
                .ThenInclude(p => p.Lesson)
                .Include(s => s.Presentation)
                .ThenInclude(p => p.Master)
                .ThenInclude(m => m.User)
                .ThenInclude(u => u.PersonInformation)
                .Include(s => s.Term)
                .OrderBy(s => s.Presentation.Lesson.Name)
                .ToListAsync();

            if (!selections.Any())
            {
                TempData["Error"] = "هیچ انتخابی برای این ترم وجود ندارد.";
                return RedirectToAction("Selections");
            }

            var term = selections.First().Term;
            var model = new SemesterSelectionsViewModel
            {
                TermId = termId,
                TermName = $"{term.Year} - {(term.Type == TermType.Fall ? "پاییز" : "بهار")}",
                Selections = selections,
                TotalUnits = selections.Sum(s => s.Presentation.Lesson.Unit),
                TotalLessons = selections.Count
            };

            return View(model);
        }
        #endregion

        #region Selection Registration
        /// <summary>
        /// Show available presentations for student to select from (only during selection time)
        /// </summary>
        public async Task<IActionResult> RegisterSelection()
        {
            var studentId = GetCurrentStudentId();
            if (studentId == Guid.Empty)
                return Unauthorized();

            // Check if current time is within selection time
            var now = DateTime.Now;
            var selectionTime = await _unitOfWork.SelectionTimes
                .GetAll()
                .FirstOrDefaultAsync(st => st.SelectionStart <= now && now <= st.SelectionEnd);

            if (selectionTime == null)
            {
                TempData["Error"] = "زمان انتخاب واحد فعال نیست.";
                return RedirectToAction("Selections");
            }

            // Get current term
            var currentTerm = await _unitOfWork.Terms
                .GetAll()
                .OrderByDescending(t => t.Year)
                .ThenByDescending(t => t.Type)
                .FirstOrDefaultAsync();

            if (currentTerm == null)
            {
                TempData["Error"] = "هیچ ترم فعالی وجود ندارد.";
                return RedirectToAction("Selections");
            }

            // Get already selected presentations for this student in current term
            var studentSelections = await _unitOfWork.Selections
                .GetAll()
                .Where(s => s.StudentId == studentId && s.TermId == currentTerm.Id)
                .Select(s => s.PresentationId)
                .ToListAsync();

            // Get all presentations
            var presentations = await _unitOfWork.Presentations
                .GetAll()
                .Include(p => p.Lesson)
                .Include(p => p.Master)
                .ThenInclude(m => m.User)
                .ThenInclude(u => u.PersonInformation)
                .OrderBy(p => p.Lesson.Name)
                .ToListAsync();

            var model = new RegisterSelectionViewModel
            {
                AvailablePresentations = presentations,
                StudentSelections = studentSelections,
                CurrentTermId = currentTerm.Id,
                CurrentTerm = currentTerm
            };

            return View(model);
        }

        /// <summary>
        /// Register a new selection for the student
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> RegisterSelection(Guid presentationId, Guid termId)
        {
            var studentId = GetCurrentStudentId();
            if (studentId == Guid.Empty)
                return Unauthorized();

            // Verify selection time is active
            var now = DateTime.Now;
            var selectionTime = await _unitOfWork.SelectionTimes
                .GetAll()
                .FirstOrDefaultAsync(st => st.SelectionStart <= now && now <= st.SelectionEnd);

            if (selectionTime == null)
            {
                TempData["Error"] = "زمان انتخاب واحد فعال نیست.";
                return RedirectToAction("RegisterSelection");
            }

            // Check if presentation and term exist
            var presentation = await _unitOfWork.Presentations.GetAsync(presentationId);
            var term = await _unitOfWork.Terms.GetAsync(termId);

            if (presentation == null || term == null)
                return NotFound();

            // Check if student already selected this presentation in this term
            var existingSelection = await _unitOfWork.Selections
                .GetAll()
                .FirstOrDefaultAsync(s => s.StudentId == studentId &&
                                         s.PresentationId == presentationId &&
                                         s.TermId == termId);

            if (existingSelection != null)
            {
                TempData["Error"] = "شما قبلاً این واحد را انتخاب کرده اید.";
                return RedirectToAction("RegisterSelection");
            }

            var selection = new Selection
            {
                StudentId = studentId,
                PresentationId = presentationId,
                TermId = termId,
                Score = 0
            };

            await _unitOfWork.Selections.AddAsync(selection);
            await _unitOfWork.CommitChangesAsync();

            TempData["Success"] = "واحد با موفقیت انتخاب شد.";
            return RedirectToAction("Selections");
        }

        /// <summary>
        /// Remove a selection (if allowed during selection time)
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> RemoveSelection(Guid selectionId)
        {
            var studentId = GetCurrentStudentId();
            if (studentId == Guid.Empty)
                return Unauthorized();

            var selection = await _unitOfWork.Selections.GetAsync(selectionId);
            if (selection == null || selection.StudentId != studentId)
                return NotFound();

            // Check if still within selection time
            var now = DateTime.Now;
            var selectionTime = await _unitOfWork.SelectionTimes
                .GetAll()
                .FirstOrDefaultAsync(st => st.SelectionStart <= now && now <= st.SelectionEnd);

            if (selectionTime == null)
            {
                TempData["Error"] = "زمان انتخاب واحد منقضی شده است.";
                return RedirectToAction("Selections");
            }

            _unitOfWork.Selections.Delete(selection);
            await _unitOfWork.CommitChangesAsync();

            TempData["Success"] = "واحد حذف شد.";
            return RedirectToAction("Selections");
        }
        #endregion

        #region Attendance History
        /// <summary>
        /// Display attendance history for all selections
        /// </summary>
        public async Task<IActionResult> AttendanceHistory()
        {
            var studentId = GetCurrentStudentId();
            if (studentId == Guid.Empty)
                return Unauthorized();

            var selections = await _unitOfWork.Selections
                .GetAll()
                .Where(s => s.StudentId == studentId)
                .Include(s => s.Presentation)
                .ThenInclude(p => p.Lesson)
                .Include(s => s.Presentation)
                .ThenInclude(p => p.Master)
                .ThenInclude(m => m.User)
                .ThenInclude(u => u.PersonInformation)
                .Include(s => s.Sessions)
                .ThenInclude(ss => ss.Attendance)
                .Include(s => s.Term)
                .OrderByDescending(s => s.Term.Year)
                .ThenByDescending(s => s.Term.Type)
                .ToListAsync();

            return View(selections);
        }

        /// <summary>
        /// Display attendance details for a specific selection
        /// </summary>
        public async Task<IActionResult> AttendanceDetails(Guid selectionId)
        {
            var studentId = GetCurrentStudentId();
            if (studentId == Guid.Empty)
                return Unauthorized();

            var selection = await _unitOfWork.Selections
                .GetAll()
                .Where(s => s.Id == selectionId && s.StudentId == studentId)
                .Include(s => s.Presentation)
                .ThenInclude(p => p.Lesson)
                .Include(s => s.Presentation)
                .ThenInclude(p => p.Master)
                .ThenInclude(m => m.User)
                .ThenInclude(u => u.PersonInformation)
                .Include(s => s.Sessions)
                .ThenInclude(ss => ss.Attendance)
                .Include(s => s.Term)
                .FirstOrDefaultAsync();

            if (selection == null)
                return NotFound();

            var model = new AttendanceDetailsViewModel
            {
                SelectionId = selection.Id,
                LessonName = selection.Presentation.Lesson.Name,
                MasterName = $"{selection.Presentation.Master.User.PersonInformation.FirstName} {selection.Presentation.Master.User.PersonInformation.LastName}",
                DayHold = selection.Presentation.DayHold,
                TermName = $"{selection.Term.Year} - {(selection.Term.Type == TermType.Fall ? "پاییز" : "بهار")}",
                Sessions = selection.Sessions
                    .OrderBy(s => s.SessionDate)
                    .Select(s => new SessionAttendanceViewModel
                    {
                        SessionId = s.Id,
                        SessionDate = s.SessionDate,
                        IsPresent = s.Attendance?.HasAttended ?? false
                    })
                    .ToList(),
                TotalSessions = selection.Sessions.Count(),
                PresentSessions = selection.Sessions.Count(s => s.Attendance != null && s.Attendance.HasAttended)
            };

            return View(model);
        }
        #endregion

        #region Grades
        /// <summary>
        /// Display grades summary with GPA by term and overall
        /// </summary>
        public async Task<IActionResult> Grades()
        {
            var studentId = GetCurrentStudentId();
            if (studentId == Guid.Empty)
                return Unauthorized();

            var selections = await _unitOfWork.Selections
                .GetAll()
                .Where(s => s.StudentId == studentId && s.Score > 0)
                .Include(s => s.Presentation)
                .ThenInclude(p => p.Lesson)
                .Include(s => s.Term)
                .OrderByDescending(s => s.Term.Year)
                .ThenByDescending(s => s.Term.Type)
                .ToListAsync();

            var gradesViewModel = new StudentGradesViewModel
            {
                AllSelections = selections,
                TermGrades = selections
                    .GroupBy(s => new { s.Term.Id, s.Term.Year, s.Term.Type })
                    .Select(g => new TermGradesViewModel
                    {
                        TermId = g.Key.Id,
                        Year = g.Key.Year,
                        Type = g.Key.Type,
                        Selections = g.ToList(),
                        GPA = CalculateGPA(g.ToList()),
                        TotalUnits = g.Sum(s => s.Presentation.Lesson.Unit)
                    })
                    .OrderByDescending(tg => tg.Year)
                    .ThenByDescending(tg => tg.Type)
                    .ToList()
            };

            gradesViewModel.OverallGPA = CalculateOverallGPA(selections);

            return View(gradesViewModel);
        }

        /// <summary>
        /// Display detailed grades for a specific term
        /// </summary>
        public async Task<IActionResult> TermGrades(Guid termId)
        {
            var studentId = GetCurrentStudentId();
            if (studentId == Guid.Empty)
                return Unauthorized();

            var selections = await _unitOfWork.Selections
                .GetAll()
                .Where(s => s.StudentId == studentId && s.TermId == termId && s.Score > 0)
                .Include(s => s.Presentation)
                .ThenInclude(p => p.Lesson)
                .Include(s => s.Presentation)
                .ThenInclude(p => p.Master)
                .ThenInclude(m => m.User)
                .ThenInclude(u => u.PersonInformation)
                .Include(s => s.Term)
                .ToListAsync();

            if (!selections.Any())
                return NotFound();

            var term = selections.First().Term;
            var model = new TermGradesViewModel
            {
                TermId = termId,
                Year = term.Year,
                Type = term.Type,
                Selections = selections,
                GPA = CalculateGPA(selections),
                TotalUnits = selections.Sum(s => s.Presentation.Lesson.Unit)
            };

            return View(model);
        }

        /// <summary>
        /// Calculate GPA for a list of selections (using 20 point scale)
        /// GPA is the weighted average of all grades based on course units
        /// </summary>
        private decimal CalculateGPA(List<Selection> selections)
        {
            if (!selections.Any())
                return 0;

            decimal totalWeightedScore = 0;
            int totalUnits = 0;

            foreach (var selection in selections)
            {
                int units = selection.Presentation.Lesson.Unit;
                totalWeightedScore += selection.Score * units;
                totalUnits += units;
            }

            return totalUnits > 0 ? Math.Round(totalWeightedScore / totalUnits, 2) : 0;
        }

        /// <summary>
        /// Calculate overall GPA from all terms (20 point scale)
        /// </summary>
        private decimal CalculateOverallGPA(List<Selection> allSelections)
        {
            if (!allSelections.Any())
                return 0;

            return CalculateGPA(allSelections);
        }

        /// <summary>
        /// Convert numeric score (0-20) to letter grade
        /// </summary>
        private (string Grade, string Color) GetLetterGrade(decimal score)
        {
            return score switch
            {
                >= 18 => ("A", "success"),      // 18-20
                >= 16 => ("B", "info"),         // 16-17.99
                >= 14 => ("C", "warning"),      // 14-15.99
                >= 12 => ("D", "danger"),       // 12-13.99
                _ => ("F", "dark")              // Below 12
            };
        }
        #endregion
    }
}