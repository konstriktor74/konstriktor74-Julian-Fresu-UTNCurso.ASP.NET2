namespace UTNCurso.DAL.EFCore.Repository
{
    public interface IRepository<T>
    {
        public Task Add(T entity);

        public Task Remove(long id);

        public Task Update(T entity);

        public Task<T> GetById(long id);

        public Task SaveChangesAsync();
    }
}
