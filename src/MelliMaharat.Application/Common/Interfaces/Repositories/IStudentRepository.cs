using MelliMaharat.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MelliMaharat.Application.Common.Interfaces.Repositories
{
    public interface IStudentRepository : IRepository<Student>
    {
        Task<Student?> GetByUserIdAsync(string userId, CancellationToken cancellationToken = default);
        Task<Student?> GetWithEnrollmentsAsync(Guid studentId, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<Student>> GetByDepartmentIdAsync(Guid departmentId, CancellationToken cancellationToken = default);
        Task<Student?> GetByStudentNumberAsync(string studentNumber, CancellationToken cancellationToken = default);
        Task<bool> StudentNumberExistsAsync(string studentNumber, CancellationToken cancellationToken = default);
        Task<int> CountByDepartmentAsync(Guid departmentId, CancellationToken cancellationToken = default);
    }
}
