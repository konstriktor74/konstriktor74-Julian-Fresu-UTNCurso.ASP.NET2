using UTNCurso.Common.Entities;
using UTNCurso.DAL.EFCore.Repository;

namespace UTNCurso.BLL.Services.Tests.Fakes
{
    public class FakeTodoItemRepository : ITodoItemRepository
    {
        public Task Add(TodoItem entity)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<TodoItem>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<TodoItem> GetById(long id)
        {
            throw new NotImplementedException();
        }

        public Task Remove(long id)
        {
            throw new NotImplementedException();
        }

        public Task SaveChangesAsync()
        {
            throw new NotImplementedException();
        }

        public Task Update(TodoItem entity)
        {
            throw new NotImplementedException();
        }
    }
}
