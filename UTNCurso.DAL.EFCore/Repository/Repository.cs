using Microsoft.EntityFrameworkCore;

namespace UTNCurso.DAL.EFCore.Repository
{
    public abstract class Repository<T> : IRepository<T>
        where T : class
    {
        private readonly DbContext _dbContext;

        public Repository(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Add(T entity)
        {
            await _dbContext.AddAsync(entity);
        }

        public async Task<T> GetById(long id)
        {
            var entity = await _dbContext.FindAsync<T>(id);
            _dbContext.Entry(entity).State = EntityState.Detached;

            return entity;
        }

        public async Task Remove(long id)
        {
            var entity = await _dbContext.FindAsync<T>(id);
            _dbContext.Remove(entity);
        }

        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        public async Task Update(T entity)
        {
            await Task.FromResult(_dbContext.Update(entity));
        }
    }
}
