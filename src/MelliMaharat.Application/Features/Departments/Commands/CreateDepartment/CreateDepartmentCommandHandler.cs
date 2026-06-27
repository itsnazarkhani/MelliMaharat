using MediatR;
using MelliMaharat.Application.Common.Interfaces;
using MelliMaharat.Application.Common.Interfaces.Repositories;
using MelliMaharat.Domain.Entities;

namespace MelliMaharat.Application.Features.Departments.Commands.CreateDepartment;

public class CreateDepartmentCommandHandler
    : IRequestHandler<CreateDepartmentCommand, Guid>
{
    private readonly IRepository<Department> _departmentRepository;
    private readonly IApplicationDbContext _context;

    public CreateDepartmentCommandHandler(
        IRepository<Department> departmentRepository,
        IApplicationDbContext context)
    {
        _departmentRepository = departmentRepository;
        _context = context;
    }

    public async Task<Guid> Handle(
        CreateDepartmentCommand request,
        CancellationToken cancellationToken)
    {
        var department = new Department
        {
            Name = request.Name,
            Description = request.Description
        };

        await _departmentRepository.AddAsync(department, cancellationToken);

        await _context.SaveChangesAsync(cancellationToken);

        return department.Id;
    }
}