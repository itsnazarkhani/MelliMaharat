using MelliMaharat.Dal.UnitOfWork;
using MelliMaharat.Models;
using MelliMaharat.Models.Enums;
using MelliMaharat.Web.Filters;
using MelliMaharat.Web.ViewModels.Student;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[AuthorizeByRole(UserRoles.Student)]
public class StudentController : Controller
{
    private readonly IUnitOfWork _unitOfWork;

    public StudentController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IActionResult> SelectUnits()
    {
        var selectionTimes = await _unitOfWork.SelectionTimes
            .GetAll()
            .OrderBy(s => s.SelectionStart)
            .ToListAsync();

        var now = DateTime.Now;

        var active = selectionTimes
            .Where(s => s.SelectionStart <= now && s.SelectionEnd >= now)
            .FirstOrDefault();

        var vm = new SelectionTimesViewModel
        {
            Times = selectionTimes,
            ActiveSelection = active
        };

        return View(vm);
    }
    public async Task<IActionResult> ChoosePresentations()
    {
        var now = DateTime.Now;

        var activeSelectionTime = await _unitOfWork.SelectionTimes
            .GetAll()
            .Where(s => s.SelectionStart <= now && s.SelectionEnd >= now)
            .FirstOrDefaultAsync();

        if (activeSelectionTime == null)
            return RedirectToAction("SelectUnits");

        var userId = HttpContext.User.GetUserId();

        var student = await _unitOfWork.Students
            .GetAll()
            .FirstOrDefaultAsync(s => s.UserId == userId);

        if (student == null)
            return BadRequest("Student not found.");

        var today = DateOnly.FromDateTime(DateTime.Now);

        var activeTerm = await _unitOfWork.Terms
            .GetAll()
            .Where(t => t.StartTime <= today && t.EndTime >= today)
            .FirstOrDefaultAsync();

        if (activeTerm == null)
        {
            activeTerm = await _unitOfWork.Terms
                .GetAll()
                .OrderByDescending(t => t.Year)
                .ThenByDescending(t => t.Type)
                .FirstOrDefaultAsync();
        }

        if (activeTerm == null)
            return BadRequest("No academic term found.");

        var presentations = await _unitOfWork.Presentations
            .GetAll()
            .Include(p => p.Master).ThenInclude(m => m.User).ThenInclude(u => u.PersonInformation)
            .Include(p => p.Lesson)
            .ToListAsync();

        var vm = new CommitPresentationSelectionViewModel
        {
            StudentId = student.Id,
            TermId = activeTerm.Id,
            Presentations = presentations
        };

        return View(vm);
    }

    [HttpPost]
    public async Task<IActionResult> CommitPresentationSelections(CommitPresentationSelectionViewModel model)
    {
        if (model.PresentationIds == null || model.PresentationIds.Count == 0)
        {
            ModelState.AddModelError("", "حداقل یک ارائه را انتخاب کنید.");

            model.Presentations = await _unitOfWork.Presentations
                .GetAll()
                .Include(p => p.Master).ThenInclude(m => m.User).ThenInclude(u => u.PersonInformation)
                .Include(p => p.Lesson)
                .ToListAsync();

            return View("ChoosePresentations", model);
        }

        foreach (var pid in model.PresentationIds)
        {
            var selection = new Selection
            {
                StudentId = model.StudentId,
                PresentationId = pid,
                TermId = model.TermId,
                Score = 0
            };

            await _unitOfWork.Selections.AddAsync(selection);  // FIXED
        }

        await _unitOfWork.CommitChangesAsync();

        return RedirectToAction("SelectionSuccess");
    }
}
