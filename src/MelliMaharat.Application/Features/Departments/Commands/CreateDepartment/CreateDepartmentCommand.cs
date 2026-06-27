using MediatR;

namespace MelliMaharat.Application.Features.Departments.Commands.CreateDepartment;

public class CreateDepartmentCommand : IRequest<Guid>
{
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
}