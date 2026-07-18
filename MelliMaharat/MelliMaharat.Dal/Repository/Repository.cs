using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MelliMaharat.Dal.Repository
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        public readonly ApplicationDbContext context;
        public readonly DbSet<T> dbSet;

        public Repository(ApplicationDbContext cotenxt)
        {
            this.context = cotenxt;
            dbSet = cotenxt.Set<T>();
        }

        public async Task<T> GetAsync(Guid id) => await dbSet.FindAsync(id);

        public IQueryable<T> GetAll() => dbSet.AsQueryable<T>();

        public async Task AddAsync(T entity)
        {
            await dbSet.AddAsync(entity);
        }

        public void Update(T entity)
        {
            dbSet.Update(entity);
        }

        public void Delete(T entity)
        {
            dbSet.Remove(entity);
        }

        public async Task<int> CountAsync() => await dbSet.CountAsync();
    }
}
