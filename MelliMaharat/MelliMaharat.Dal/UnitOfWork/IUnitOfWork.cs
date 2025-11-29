using MelliMaharat.Dal.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace MelliMaharat.Dal.UnitOfWork
{
    /// <summary>
    /// Defines the Unit of Work pattern for managing multiple repositories and coordinating database operations.
    /// This interface provides access to all domain entity repositories and facilitates atomic transactions
    /// across multiple repositories through the CommitChangesAsync method.
    /// </summary>
    /// <remarks>
    /// Implements IAsyncDisposable to properly manage database connections and resources.
    /// Usage:
    /// <code>
    /// using (var unitOfWork = new UnitOfWork(dbContext))
    /// {
    ///     await unitOfWork.Users.Add(user);
    ///     await unitOfWork.CommitChangesAsync();
    /// }
    /// </code>
    /// </remarks>
    public interface IUnitOfWork : IAsyncDisposable
    {
        IRepository<Attendance> Attendances { get; }
        IRepository<Department> Departments { get; }
        IRepository<Lesson> Lessons { get; }
        IRepository<Master> Masters { get; }
        IRepository<Presentation> Presentations { get; }
        IRepository<Selection> Selections { get; }
        IRepository<SelectionFeedback> SelectionFeedbacks { get; }
        IRepository<SelectionTime> SelectionTimes { get; }
        IRepository<Session> Sessions { get; }
        IRepository<Term> Terms { get; }
        IRepository<User> Users { get; }

        /// <summary>
        /// Persists all pending changes to the underlying data store asynchronously.
        /// This method coordinates the commit operation across all repositories and ensures
        /// data consistency through transaction management.
        /// </summary>
        /// <returns>A task representing the asynchronous commit operation.</returns>
        /// <exception cref="DbUpdateException">Thrown if the database update operation fails.</exception>
        /// <exception cref="DbUpdateConcurrencyException">Thrown if a concurrency conflict occurs during the update.</exception>
        Task CommitChangesAsync();
    }
}
