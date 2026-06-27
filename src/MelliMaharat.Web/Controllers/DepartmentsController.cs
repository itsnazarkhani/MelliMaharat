using MediatR;
using MelliMaharat.Application.Features.Departments.Commands.CreateDepartment;
using Microsoft.AspNetCore.Mvc;

namespace MelliMaharat.Web.Controllers
{
    public class DepartmentsController : Controller
    {
        private readonly IMediator _mediator;

        public DepartmentsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateDepartmentCommand command)
        {
            var id = await _mediator.Send(command);

            return RedirectToAction("Details", new { id });
        }
    }
}
