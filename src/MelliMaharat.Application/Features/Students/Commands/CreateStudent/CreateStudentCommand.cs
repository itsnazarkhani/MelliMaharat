using MediatR;

namespace MelliMaharat.Application.Features.Students.Commands.CreateStudent;

public class CreateStudentCommand : IRequest<Guid>
{
    public string ApplicationUserId { get; set; } = null!;
    public string StudentNumber { get; set; } = null!;
    public Guid DepartmentId { get; set; }
}