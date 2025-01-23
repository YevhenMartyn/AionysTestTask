using Infrastructure.Data;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class Repository<T>(ApplicationDbContext dbContext) : IRepository<T>
        where T : class, IBaseEntity
    {
        protected readonly DbSet<T> Entities = dbContext.Set<T>();
        protected readonly ApplicationDbContext DbContext = dbContext;

        public async Task<T> CreateAsync(T entity)
        {
            var newEntity = await Entities.AddAsync(entity);
            await DbContext.SaveChangesAsync();
            return newEntity.Entity;
        }

        public async Task<T> UpdateAsync(T entity)
        {
            var updatedEntity = Entities.Update(entity);
            await DbContext.SaveChangesAsync();
            return updatedEntity.Entity;
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            IQueryable<T> query = Entities;
            return await query.ToListAsync();
        }

        public async Task<T> GetByIdAsync(Guid id)
        {
            T? entity = await Entities
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();
            if (entity == null)
            {
                throw new Exception($"Entity with {id} Id not found");
            }

            return entity;
        }

        public async Task DeleteAsync(Guid id)
        {
            var entity = await GetByIdAsync(id);
            if (entity == null)
            {
                throw new ArgumentException();
            }

            DbContext.Remove(entity);
            await DbContext.SaveChangesAsync();
        }
    }
}