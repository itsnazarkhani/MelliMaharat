using MelliMaharat.Dal.UnitOfWork;
using MelliMaharat.Models;
using MelliMaharat.Models.Enums;
using MelliMaharat.Web.Filters;
using MelliMaharat.Web.ViewModels.Master;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Text;

[AuthorizeByRole(UserRoles.Master)]
public class MasterController : Controller
{
    private readonly IUnitOfWork _uow;

    public MasterController(IUnitOfWork uow)
    {
        _uow = uow;
    }

    #region Helper Methods

    /// <summary>
    /// Safely extracts and validates the current user's ID from claims
    /// </summary>
    private async Task<Guid?> GetCurrentUserIdAsync()
    {
        var userIdClaim = User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (Guid.TryParse(userIdClaim, out var userId))
            return userId;
        return null;
    }

    /// <summary>
    /// Verifies that the current user is the master of the specified presentation
    /// </summary>
    private async Task<bool> IsUserMasterOfPresentationAsync(Guid presentationId)
    {
        var userId = await GetCurrentUserIdAsync();
        if (!userId.HasValue)
            return false;

        var presentation = await _uow.Presentations.GetAll()
            .Include(p => p.Master.User)
            .FirstOrDefaultAsync(p => p.Id == presentationId && !p.IsDeleted);

        return presentation?.Master.User.Id == userId.Value;
    }

    /// <summary>
    /// Safely formats student full name with null checks
    /// </summary>
    private string GetStudentFullName(Student student)
    {
        if (student?.User?.PersonInformation == null)
            return "Unknown Student";

        var firstName = student.User.PersonInformation.FirstName ?? string.Empty;
        var lastName = student.User.PersonInformation.LastName ?? string.Empty;

        return $"{firstName} {lastName}".Trim();
    }

    #endregion

    /// <summary>
    /// Displays all lessons the master is teaching in the current term
    /// </summary>
    public async Task<IActionResult> PresentedLessons()
    {
        var userId = await GetCurrentUserIdAsync();
        if (!userId.HasValue)
            return Unauthorized();

        var master = await _uow.Masters.GetAll()
            .Include(m => m.Presentations)
                .ThenInclude(p => p.Lesson)
            .FirstOrDefaultAsync(m => m.User.Id == userId.Value && !m.IsDeleted);

        if (master == null)
            return Unauthorized();

        var currentTerm = await _uow.Terms.GetAll()
            .Where(t => !t.IsDeleted)
            .OrderByDescending(t => t.StartTime)
            .FirstOrDefaultAsync();

        if (currentTerm == null)
        {
            return View(new List<PresentedLessonViewModel>());
        }

        var model = master.Presentations
            .Where(p => !p.IsDeleted)
            .Select(p => new PresentedLessonViewModel
            {
                PresentationId = p.Id,
                LessonName = p.Lesson?.Name ?? "Unknown Lesson",
                DayHold = p.DayHold,
                StartTime = p.StartTime,
                EndTime = p.EndTime
            })
            .OrderBy(p => p.DayHold)
            .ThenBy(p => p.StartTime)
            .ToList();

        return View(model);
    }

    /// <summary>
    /// Displays all sessions for a specific presentation
    /// </summary>
    public async Task<IActionResult> Sessions(Guid presentationId)
    {
        // Authorization check
        if (!await IsUserMasterOfPresentationAsync(presentationId))
            return Forbid();

        var sessions = await _uow.Sessions.GetAll()
            .Include(s => s.Selection)
            .Include(s => s.Attendance)
            .Where(s => s.Selection.PresentationId == presentationId && !s.IsDeleted)
            .OrderByDescending(s => s.SessionDate)
            .ToListAsync();

        if (!sessions.Any())
        {
            ViewBag.PresentationId = presentationId;
            return View(new List<SessionListViewModel>());
        }

        var model = sessions
            .Select(s => new SessionListViewModel
            {
                SessionId = s.Id,
                SessionDate = s.SessionDate,
                HasAttendance = s.Attendance != null,
                PresentationId = presentationId  // Add this property to your ViewModel
            })
            .ToList();

        // Pass presentationId to view via ViewBag as fallback
        ViewBag.PresentationId = presentationId;

        return View(model);
    }

    /// <summary>
    /// Displays all presentations with grading status
    /// </summary>
    public async Task<IActionResult> GradeManagement()
    {
        var userId = await GetCurrentUserIdAsync();
        if (!userId.HasValue)
            return Unauthorized();

        var master = await _uow.Masters.GetAll()
            .Include(m => m.Presentations)
                .ThenInclude(p => p.Lesson)
            .Include(m => m.Presentations)
                .ThenInclude(p => p.Selections)
            .FirstOrDefaultAsync(m => m.User.Id == userId.Value && !m.IsDeleted);

        if (master == null)
            return Unauthorized();

        var model = master.Presentations
            .Where(p => !p.IsDeleted)
            .Select(p =>
            {
                var students = p.Selections.Where(s => !s.IsDeleted).ToList();
                var gradedCount = students.Count(s => s.Score > 0);

                return new PresentationGradesStatusViewModel
                {
                    PresentationId = p.Id,
                    LessonName = p.Lesson?.Name ?? "Unknown",
                    DayHold = p.DayHold,
                    TotalStudents = students.Count,
                    GradedStudents = gradedCount,
                    LastModified = p.TimeStamp != null ? DateTime.Now : null
                };
            })
            .OrderBy(p => p.LessonName)
            .ToList();

        return View(model);
    }

    /// <summary>
    /// Displays the grade submission form for a presentation
    /// </summary>
    public async Task<IActionResult> SubmitGrades(Guid presentationId)
    {
        // Authorization check
        if (!await IsUserMasterOfPresentationAsync(presentationId))
            return Forbid();

        var presentation = await _uow.Presentations.GetAll()
            .Include(p => p.Lesson)
            .Include(p => p.Selections)
                .ThenInclude(s => s.Student)
                    .ThenInclude(st => st.User)
                        .ThenInclude(u => u.PersonInformation)
            .FirstOrDefaultAsync(p => p.Id == presentationId && !p.IsDeleted);

        if (presentation == null)
            return NotFound();

        var selections = presentation.Selections
            .Where(s => !s.IsDeleted)
            .ToList();

        if (!selections.Any())
        {
            ModelState.AddModelError("", "هیچ دانشجویی در این کلاس ثبت‌نام نشده است");
            return View(new SubmitGradesViewModel());
        }

        var items = selections
            .Select(sel => new GradeItemViewModel
            {
                SelectionId = sel.Id,
                StudentName = GetStudentFullName(sel.Student),
                CurrentScore = sel.Score,
                NewScore = sel.Score,
                IsModified = false
            })
            .OrderBy(item => item.StudentName)
            .ToList();

        var model = new SubmitGradesViewModel
        {
            PresentationId = presentationId,
            LessonName = presentation.Lesson?.Name ?? "Unknown Lesson",
            Items = items,
            TotalStudents = items.Count,
            GradesSubmitted = items.Count(i => i.CurrentScore > 0),
            AverageGrade = items.Any() ? Math.Round(items.Average(i => i.CurrentScore), 2) : 0
        };

        return View(model);
    }

    /// <summary>
    /// Saves the submitted grades
    /// </summary>
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> SubmitGrades(SubmitGradesViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        // Authorization check
        if (!await IsUserMasterOfPresentationAsync(model.PresentationId))
            return Forbid();

        try
        {
            // Verify presentation exists
            var presentation = await _uow.Presentations.GetAll()
                .FirstOrDefaultAsync(p => p.Id == model.PresentationId && !p.IsDeleted);

            if (presentation == null)
                return NotFound();

            // Verify all selections belong to this presentation
            var validSelectionIds = await _uow.Selections.GetAll()
                .Where(sel => sel.PresentationId == model.PresentationId && !sel.IsDeleted)
                .Select(sel => sel.Id)
                .ToListAsync();

            if (model.Items.Any(item => !validSelectionIds.Contains(item.SelectionId)))
                return BadRequest("Invalid selection data");

            var updatedCount = 0;

            foreach (var item in model.Items)
            {
                // Validate score range
                if (item.NewScore < 0 || item.NewScore > 20)
                {
                    ModelState.AddModelError("", $"نمره برای {item.StudentName} باید بین 0 تا 20 باشد");
                    continue;
                }

                var selection = await _uow.Selections.GetAsync(item.SelectionId);
                if (selection == null)
                {
                    ModelState.AddModelError("", $"دانشجو {item.StudentName} یافت نشد");
                    continue;
                }

                if (selection.Score != item.NewScore)
                {
                    selection.Score = item.NewScore;
                    _uow.Selections.Update(selection);
                    updatedCount++;
                }
            }

            if (ModelState.IsValid && updatedCount > 0)
            {
                await _uow.CommitChangesAsync();
                TempData["SuccessMessage"] = $"{updatedCount} نمره با موفقیت ثبت شد";
                return RedirectToAction(nameof(GradesSummary), new { presentationId = model.PresentationId });
            }
        }
        catch (DbUpdateException ex)
        {
            ModelState.AddModelError("", "خطا در ذخیره نمرات. لطفاً دوباره تلاش کنید.");
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", "خطای غیرمنتظره رخ داد. لطفاً دوباره تلاش کنید.");
        }

        return View(model);
    }

    /// <summary>
    /// Displays a summary of submitted grades for a presentation
    /// </summary>
    public async Task<IActionResult> GradesSummary(Guid presentationId)
    {
        // Authorization check
        if (!await IsUserMasterOfPresentationAsync(presentationId))
            return Forbid();

        var presentation = await _uow.Presentations.GetAll()
            .Include(p => p.Lesson)
            .Include(p => p.Selections)
                .ThenInclude(s => s.Student)
                    .ThenInclude(st => st.User)
                        .ThenInclude(u => u.PersonInformation)
            .FirstOrDefaultAsync(p => p.Id == presentationId && !p.IsDeleted);

        if (presentation == null)
            return NotFound();

        var selections = presentation.Selections
            .Where(s => !s.IsDeleted)
            .ToList();

        var items = selections
            .Select(sel => new GradeSummaryItemViewModel
            {
                SelectionId = sel.Id,
                StudentName = GetStudentFullName(sel.Student),
                Grade = sel.Score,
                IsGraded = sel.Score > 0
            })
            .OrderBy(item => item.StudentName)
            .ToList();

        var model = new GradesSummaryViewModel
        {
            PresentationId = presentationId,
            LessonName = presentation.Lesson?.Name ?? "Unknown Lesson",
            Items = items,
            IsSubmitted = items.Any(i => i.IsGraded),
            SubmissionDate = DateTime.Now
        };

        return View(model);
    }

    /// <summary>
    /// Exports grades to CSV format
    /// </summary>
    public async Task<IActionResult> ExportGrades(Guid presentationId)
    {
        // Authorization check
        if (!await IsUserMasterOfPresentationAsync(presentationId))
            return Forbid();

        var presentation = await _uow.Presentations.GetAll()
            .Include(p => p.Lesson)
            .Include(p => p.Selections)
                .ThenInclude(s => s.Student)
                    .ThenInclude(st => st.User)
                        .ThenInclude(u => u.PersonInformation)
            .FirstOrDefaultAsync(p => p.Id == presentationId && !p.IsDeleted);

        if (presentation == null)
            return NotFound();

        var selections = presentation.Selections
            .Where(s => !s.IsDeleted)
            .OrderBy(s => s.Student.User.PersonInformation.FirstName)
            .ToList();

        var csv = new StringBuilder();
        csv.AppendLine("نام درس,نام دانشجو,نمره");

        foreach (var selection in selections)
        {
            var studentName = GetStudentFullName(selection.Student);
            csv.AppendLine($"\"{presentation.Lesson?.Name ?? "Unknown"}\",\"{studentName}\",{selection.Score}");
        }

        var fileName = $"grades_{presentation.Lesson?.Name}_{DateTime.Now:yyyyMMdd}.csv";
        var fileBytes = Encoding.UTF8.GetBytes(csv.ToString());

        return File(fileBytes, "text/csv; charset=utf-8", fileName);
    }

    /// <summary>
    /// GET: Displays the attendance form for a specific session
    /// </summary>
    public async Task<IActionResult> TakeAttendance(Guid sessionId)
    {
        var session = await _uow.Sessions.GetAll()
            .Include(s => s.Selection)
                .ThenInclude(sel => sel.Student)
                    .ThenInclude(st => st.User)
                        .ThenInclude(u => u.PersonInformation)
            .Include(s => s.Attendance)
            .FirstOrDefaultAsync(s => s.Id == sessionId && !s.IsDeleted);

        if (session == null)
            return NotFound();

        // Authorization check
        if (!await IsUserMasterOfPresentationAsync(session.Selection.PresentationId))
            return Forbid();

        var selections = await _uow.Selections.GetAll()
            .Where(sel => sel.PresentationId == session.Selection.PresentationId && !sel.IsDeleted)
            .Include(sel => sel.Student)
                .ThenInclude(st => st.User)
                    .ThenInclude(u => u.PersonInformation)
            .Include(sel => sel.Sessions)
                .ThenInclude(se => se.Attendance)
            .ToListAsync();

        var items = selections
            .Select(sel =>
            {
                var sess = sel.Sessions.FirstOrDefault(x => x.Id == sessionId && !x.IsDeleted);
                var att = sess?.Attendance;

                return new AttendanceItemViewModel
                {
                    SelectionId = sel.Id,
                    StudentName = GetStudentFullName(sel.Student),
                    AttendanceId = att?.Id ?? Guid.Empty,
                    HasAttended = att?.HasAttended ?? false
                };
            })
            .OrderBy(item => item.StudentName)
            .ToList();

        var vm = new AttendanceViewModel
        {
            SessionId = sessionId,
            PresentationId = session.Selection.PresentationId,
            Items = items
        };

        return View(vm);
    }

    /// <summary>
    /// POST: Saves attendance records for a session
    /// </summary>
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> TakeAttendance(AttendanceViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        // Authorization check
        if (!await IsUserMasterOfPresentationAsync(model.PresentationId))
            return Forbid();

        try
        {
            // Verify session exists and belongs to the presentation
            var session = await _uow.Sessions.GetAll()
                .Include(s => s.Selection)
                .FirstOrDefaultAsync(s => s.Id == model.SessionId && !s.IsDeleted);

            if (session == null)
                return NotFound();

            if (session.Selection.PresentationId != model.PresentationId)
                return BadRequest("Session does not belong to the specified presentation");

            // Verify all selection IDs belong to this presentation
            var validSelectionIds = await _uow.Selections.GetAll()
                .Where(sel => sel.PresentationId == model.PresentationId && !sel.IsDeleted)
                .Select(sel => sel.Id)
                .ToListAsync();

            if (model.Items.Any(item => !validSelectionIds.Contains(item.SelectionId)))
                return BadRequest("Invalid selection data");

            foreach (var item in model.Items)
            {
                if (item.AttendanceId == Guid.Empty)
                {
                    var attendance = new Attendance
                    {
                        Id = Guid.NewGuid(),
                        SessionId = model.SessionId,
                        HasAttended = item.HasAttended,
                        IsDeleted = false
                    };
                    await _uow.Attendances.AddAsync(attendance);
                }
                else
                {
                    var attendance = await _uow.Attendances.GetAsync(item.AttendanceId);
                    if (attendance == null)
                    {
                        ModelState.AddModelError("", $"Attendance record not found");
                        return View(model);
                    }

                    if (attendance.IsDeleted)
                    {
                        ModelState.AddModelError("", "Cannot update a deleted attendance record");
                        return View(model);
                    }

                    attendance.HasAttended = item.HasAttended;
                    _uow.Attendances.Update(attendance);
                }
            }

            await _uow.CommitChangesAsync();
            return RedirectToAction(nameof(Sessions), new { presentationId = model.PresentationId });
        }
        catch (DbUpdateException ex)
        {
            ModelState.AddModelError("", "An error occurred while saving attendance. Please try again.");
            return View(model);
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", "An unexpected error occurred. Please try again.");
            return View(model);
        }
    }

    /// <summary>
    /// GET: Displays the form to set a student's score
    /// </summary>
    public async Task<IActionResult> SetScore(Guid selectionId)
    {
        var selection = await _uow.Selections.GetAll()
            .Include(s => s.Student)
                .ThenInclude(st => st.User)
                    .ThenInclude(u => u.PersonInformation)
            .Include(s => s.Presentation)
                .ThenInclude(p => p.Master)
                    .ThenInclude(m => m.User)
            .FirstOrDefaultAsync(s => s.Id == selectionId && !s.IsDeleted);

        if (selection == null)
            return NotFound();

        // Authorization check
        if (!await IsUserMasterOfPresentationAsync(selection.Presentation.Id))
            return Forbid();

        var vm = new SetScoreViewModel
        {
            SelectionId = selectionId,
            StudentName = GetStudentFullName(selection.Student),
            CurrentScore = selection.Score
        };

        return View(vm);
    }

    /// <summary>
    /// POST: Updates a student's score
    /// </summary>
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> SetScore(SetScoreViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        try
        {
            var selection = await _uow.Selections.GetAll()
                .Include(s => s.Presentation)
                .FirstOrDefaultAsync(s => s.Id == model.SelectionId && !s.IsDeleted);

            if (selection == null)
                return NotFound();

            // Authorization check
            if (!await IsUserMasterOfPresentationAsync(selection.Presentation.Id))
                return Forbid();

            // Validate score range (0-20)
            if (model.NewScore < 0 || model.NewScore > 20)
            {
                ModelState.AddModelError(nameof(model.NewScore), "Score must be between 0 and 20");
                return View(model);
            }

            selection.Score = model.NewScore;
            _uow.Selections.Update(selection);
            await _uow.CommitChangesAsync();

            return RedirectToAction(nameof(PresentedLessons));
        }
        catch (DbUpdateException ex)
        {
            ModelState.AddModelError("", "An error occurred while saving the score. Please try again.");
            return View(model);
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", "An unexpected error occurred. Please try again.");
            return View(model);
        }
    }

    /// <summary>
    /// GET: Displays the form to create a new session for a presentation
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> CreateSession(Guid presentationId)
    {
        // Authorization check
        if (!await IsUserMasterOfPresentationAsync(presentationId))
            return Forbid();

        // Verify presentation exists
        var presentation = await _uow.Presentations.GetAll()
            .FirstOrDefaultAsync(p => p.Id == presentationId && !p.IsDeleted);

        if (presentation == null)
            return NotFound();

        var model = new CreateSessionViewModel
        {
            PresentationId = presentationId,
            SessionDate = DateTime.Today
        };

        return View(model);
    }

    /// <summary>
    /// POST: Creates new sessions for all students in a presentation on the specified date
    /// </summary>
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateSession(CreateSessionViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        // Authorization check
        if (!await IsUserMasterOfPresentationAsync(model.PresentationId))
            return Forbid();

        try
        {
            // Verify presentation exists
            var presentation = await _uow.Presentations.GetAll()
                .FirstOrDefaultAsync(p => p.Id == model.PresentationId && !p.IsDeleted);

            if (presentation == null)
                return NotFound();

            // Validate that session date is not in the past
            if (model.SessionDate.Date < DateTime.Today)
            {
                ModelState.AddModelError(nameof(model.SessionDate), "Session date cannot be in the past");
                return View(model);
            }

            // Get all selections for this presentation
            var selections = await _uow.Selections.GetAll()
                .Where(s => s.PresentationId == model.PresentationId && !s.IsDeleted)
                .ToListAsync();

            if (!selections.Any())
            {
                ModelState.AddModelError("", "No students are enrolled in this presentation");
                return View(model);
            }

            // Check for existing sessions on the same date to prevent duplicates
            var existingSessionIds = await _uow.Sessions.GetAll()
                .Where(s => s.Selection.PresentationId == model.PresentationId &&
                           s.SessionDate.Date == model.SessionDate.Date &&
                           !s.IsDeleted)
                .Select(s => s.SelectionId)
                .ToListAsync();

            var sessionsToCreate = selections
                .Where(sel => !existingSessionIds.Contains(sel.Id))
                .ToList();

            if (!sessionsToCreate.Any())
            {
                ModelState.AddModelError("", "Sessions already exist for the selected date");
                return View(model);
            }

            // Create sessions for each selection
            foreach (var sel in sessionsToCreate)
            {
                var session = new Session
                {
                    Id = Guid.NewGuid(),
                    SelectionId = sel.Id,
                    SessionDate = model.SessionDate,
                    IsDeleted = false
                };
                await _uow.Sessions.AddAsync(session);
            }

            await _uow.CommitChangesAsync();

            return RedirectToAction(nameof(Sessions), new { presentationId = model.PresentationId });
        }
        catch (DbUpdateException ex)
        {
            ModelState.AddModelError("", "An error occurred while creating sessions. Please try again.");
            return View(model);
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", "An unexpected error occurred. Please try again.");
            return View(model);
        }
    }
}