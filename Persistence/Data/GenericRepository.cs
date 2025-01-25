using Domain;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Data
{
    public class GenericRepository<T>(DataContext dataContext) : IGenericRepository<T> where T : BaseEntity
    {
        public void Add(T entity)
        {
            dataContext.Set<T>().Add(entity);
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await dataContext.Set<T>().FindAsync(id);
        }

        public async Task<List<T>> ListAllAsync()
        {
            return await dataContext.Set<T>().ToListAsync();
        }

        public void Remove(T entity)
        {
            dataContext.Set<T>().Remove(entity);
        }

        public void Update(T entity)
        {
            dataContext.Set<T>().Attach(entity);
        }

        public async Task<bool> SaveAllAsync()
        {
            return await dataContext.SaveChangesAsync() > 0;
        }
    }
}