using UTNCurso.Common.Entities;
using UTNCurso.DAL.EFCore.Repository;

namespace UTNCurso.BLL.Services.Tests.Fakes
{
    public class FakeTodoItemRepository : ITodoItemRepository
    {
        public List<TodoItem> Store { get; set; } = new List<TodoItem>();

        public TodoItem LastAdded { get; set; }

        public async Task Add(TodoItem entity)
        {
            Store.Add(entity);
            await Task.CompletedTask;
        }

        public async Task<IEnumerable<TodoItem>> GetAll()
        {
            return await Task.FromResult(Store);
        }

        public async Task<TodoItem> GetById(long id)
        {
            return await Task.FromResult(Store.FirstOrDefault(x => x.Id == id));
        }

        public Task Remove(long id)
        {
            throw new NotImplementedException();
        }

        public async Task SaveChangesAsync()
        {
            await Task.CompletedTask;
        }

        public async Task Update(TodoItem entity)
        {
            var storedEntity = Store.FirstOrDefault(x => x.Id == entity.Id);
            Store.Remove(storedEntity);
            Store.Add(entity);
        }
    }
}
