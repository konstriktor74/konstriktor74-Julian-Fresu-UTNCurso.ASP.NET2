using Microsoft.EntityFrameworkCore;
using UTNCurso.Common.Entities;

namespace UTNCurso.DAL.EFCore.Repository
{
    public class TodoItemRepository : Repository<TodoItem>, ITodoItemRepository
    {
        private readonly TodoContext _todoContext;

        public TodoItemRepository(TodoContext dbContext) : base(dbContext)
        {
            _todoContext = dbContext;
        }

        public async Task<IEnumerable<TodoItem>> GetAll()
        {
            return await _todoContext.TodoItem.ToListAsync();
        }
    }
}
