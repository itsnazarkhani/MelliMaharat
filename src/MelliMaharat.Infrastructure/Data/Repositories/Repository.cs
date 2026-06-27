using MelliMaharat.Application.Common;
using MelliMaharat.Domain.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace MelliMaharat.Infrastructure.Data.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
    {
        protected readonly AppDbContext _context;
        protected readonly DbSet<TEntity> _dbSet;

        public Repository(AppDbContext context)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
        }

        public async Task<TEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default) =>
            await _dbSet.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

        public async Task<IReadOnlyList<TEntity>> GetAllAsync(CancellationToken cancellationToken = default) =>
            await _dbSet.ToListAsync(cancellationToken);

        public async Task AddAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            await _dbSet.AddAsync(entity, cancellationToken);
        }

        public void Update(TEntity entity)
        {
            _dbSet.Update(entity);
        }

        public void Delete(TEntity entity)
        {
            _dbSet.Remove(entity);
        }

        public async Task<int> CountAsync(CancellationToken cancellationToken = default) =>
            await _dbSet.CountAsync(cancellationToken);

        public async Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default) =>
            await _dbSet.AnyAsync(x => x.Id == id);

        public async Task<IReadOnlyList<TEntity>> FindAsync(
            Expression<Func<TEntity, bool>> predicate,
            CancellationToken cancellationToken = default) =>
            await _dbSet.Where(predicate).ToListAsync(cancellationToken);

        public async Task<TEntity?> FirstOrDefaultAsync(
            Expression<Func<TEntity, bool>> predicate,
            CancellationToken cancellationToken = default) =>
            await _dbSet.FirstOrDefaultAsync(predicate, cancellationToken);

        public async Task<IReadOnlyList<TEntity>> GetPagedAsync(
            int page,
            int pageSize,
            CancellationToken cancellationToken = default) =>
            await _dbSet
                .Skip(page)
                .Take(pageSize)
                .ToListAsync(cancellationToken);
    }
}
