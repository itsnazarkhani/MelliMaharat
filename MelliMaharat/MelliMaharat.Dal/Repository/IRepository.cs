using System;
using System.Collections.Generic;
using System.Text;

namespace MelliMaharat.Dal.Repository
{
    public interface IRepository<T> where T : BaseEntity
    {
        Task<T> GetAsync(Guid id);
        IQueryable<T> GetAll();
        Task AddAsync(T entity);
        void Update(T entity);
        void Delete(T entity);
        Task<int> CountAsync();
    }
}
