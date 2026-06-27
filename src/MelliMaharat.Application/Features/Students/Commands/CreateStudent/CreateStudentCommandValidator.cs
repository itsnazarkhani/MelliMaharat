using FluentValidation;

namespace MelliMaharat.Application.Features.Students.Commands.CreateStudent;

public class CreateStudentCommandValidator
    : AbstractValidator<CreateStudentCommand>
{
    public CreateStudentCommandValidator()
    {
        RuleFor(x => x.ApplicationUserId)
            .NotEmpty();

        RuleFor(x => x.StudentNumber)
            .NotEmpty()
            .MaximumLength(20);

        RuleFor(x => x.DepartmentId)
            .NotEmpty();
    }
}