namespace UTNCurso.Core.Interfaces
{
    public interface IRepository<T, I>
        where T : IAggregateRoot
    {
        public Task Add(T entity);

        public Task Remove(I id);

        public Task Update(T entity);

        public Task<T> GetById(I id);

        public Task SaveChangesAsync();
    }
}
