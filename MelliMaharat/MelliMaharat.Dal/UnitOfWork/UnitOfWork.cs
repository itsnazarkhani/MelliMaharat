using MelliMaharat.Dal.Repository;
using System;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MelliMaharat.Dal.UnitOfWork
{
    namespace MelliMaharat.Dal.UnitOfWork
    {
        /// <summary>
        /// Implements the Unit of Work pattern for coordinating multiple repositories
        /// and managing database transactions across the application.
        /// </summary>
        public class UnitOfWork : IUnitOfWork
        {
            private readonly ApplicationDbContext _dbContext;
            private IRepository<Attendance> _attendances;
            private IRepository<Department> _departments;
            private IRepository<Lesson> _lessons;
            private IRepository<Master> _masters;
            private IRepository<Presentation> _presentations;
            private IRepository<Selection> _selections;
            private IRepository<SelectionFeedback> _selectionFeedbacks;
            private IRepository<SelectionTime> _selectionTimes;
            private IRepository<Session> _sessions;
            private IRepository<Term> _terms;
            private IRepository<User> _users;

            public UnitOfWork(ApplicationDbContext dbContext)
            {
                _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            }

            public IRepository<Attendance> Attendances =>
                _attendances ??= new Repository<Attendance>(_dbContext);

            public IRepository<Department> Departments =>
                _departments ??= new Repository<Department>(_dbContext);

            public IRepository<Lesson> Lessons =>
                _lessons ??= new Repository<Lesson>(_dbContext);

            public IRepository<Master> Masters =>
                _masters ??= new Repository<Master>(_dbContext);

            public IRepository<Presentation> Presentations =>
                _presentations ??= new Repository<Presentation>(_dbContext);

            public IRepository<Selection> Selections =>
                _selections ??= new Repository<Selection>(_dbContext);

            public IRepository<SelectionFeedback> SelectionFeedbacks =>
                _selectionFeedbacks ??= new Repository<SelectionFeedback>(_dbContext);

            public IRepository<SelectionTime> SelectionTimes =>
                _selectionTimes ??= new Repository<SelectionTime>(_dbContext);

            public IRepository<Session> Sessions =>
                _sessions ??= new Repository<Session>(_dbContext);

            public IRepository<Term> Terms =>
                _terms ??= new Repository<Term>(_dbContext);

            public IRepository<User> Users =>
                _users ??= new Repository<User>(_dbContext);

            public async Task CommitChangesAsync()
            {
                try
                {
                    await _dbContext.SaveChangesAsync();
                }
                catch (DbUpdateException ex)
                {
                    throw new InvalidOperationException("Database update failed. Please try again.", ex);
                }
            }

            public async ValueTask DisposeAsync()
            {
                if (_dbContext != null)
                {
                    await _dbContext.DisposeAsync();
                }
            }
        }
    }
}
