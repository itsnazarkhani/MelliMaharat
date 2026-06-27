using MediatR;
using MelliMaharat.Application.Features.Students.Commands.CreateStudent;
using Microsoft.AspNetCore.Mvc;

namespace MelliMaharat.Web.Controllers
{
    public class StudentsController : Controller
    {
        private readonly IMediator _mediator;

        public StudentsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateStudentCommand command)
        {
            var id = await _mediator.Send(command);

            return RedirectToAction(nameof(Details), new { id });
        }

        [HttpGet]
        public IActionResult Details(Guid id)
        {
            return View();
        }
    }
}
