using MediatR;
using MelliMaharat.Application.Common.Interfaces;
using MelliMaharat.Application.Common.Interfaces.Repositories;
using MelliMaharat.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MelliMaharat.Application.Features.Students.Commands.CreateStudent
{
    public class CreateStudentCommandHandler
     : IRequestHandler<CreateStudentCommand, Guid>
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IApplicationDbContext _context;

        public CreateStudentCommandHandler(
            IStudentRepository studentRepository,
            IApplicationDbContext context)
        {
            _studentRepository = studentRepository;
            _context = context;
        }

        public async Task<Guid> Handle(
            CreateStudentCommand request,
            CancellationToken cancellationToken)
        {
            if (await _studentRepository.StudentNumberExistsAsync(
                request.StudentNumber,
                cancellationToken))
            {
                throw new InvalidOperationException("Student number already exists.");
            }

            var student = new Student
            {
                UserId = request.ApplicationUserId,
                StudentNumber = request.StudentNumber,
                DepartmentId = request.DepartmentId
            };

            await _studentRepository.AddAsync(student, cancellationToken);

            await _context.SaveChangesAsync(cancellationToken);

            return student.Id;
        }
    }
}
