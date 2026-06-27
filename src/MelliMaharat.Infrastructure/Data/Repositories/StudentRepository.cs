using MelliMaharat.Application.Common.Interfaces.Repositories;
using MelliMaharat.Domain.Entities;
using MelliMaharat.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace MelliMaharat.Infrastructure.Data.Repositories
{
    public class StudentRepository : Repository<Student>, IStudentRepository
    {
        public StudentRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<Student?> GetByUserIdAsync(string userId, CancellationToken cancellationToken = default) =>
            await _dbSet
                .FirstOrDefaultAsync(x => x.UserId == userId, cancellationToken);

        public async Task<Student?> GetWithEnrollmentsAsync(Guid studentId, CancellationToken cancellationToken = default) =>
            await _dbSet
                .Include(x => x.Enrollments)
                .FirstOrDefaultAsync(x => x.Id == studentId, cancellationToken);

        public async Task<IReadOnlyList<Student>> GetByDepartmentIdAsync(Guid departmentId, CancellationToken cancellationToken = default) =>
            await _dbSet
                .Where(x => x.DepartmentId == departmentId)
                .ToListAsync(cancellationToken);

        public async Task<Student?> GetByStudentNumberAsync(string studentNumber, CancellationToken cancellationToken = default) =>
            await _dbSet
                .FirstOrDefaultAsync(s => s.StudentNumber == studentNumber, cancellationToken);

        public async Task<bool> StudentNumberExistsAsync(string studentNumber, CancellationToken cancellationToken = default) =>
            await _dbSet
                .AnyAsync(x => x.StudentNumber == studentNumber, cancellationToken);

        public async Task<int> CountByDepartmentAsync(Guid departmentId, CancellationToken cancellationToken = default) =>
            await _dbSet
                .CountAsync(x => x.DepartmentId == departmentId, cancellationToken);

    }
}