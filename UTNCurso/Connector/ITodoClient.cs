using UTNCurso.Core.Domain;

namespace UTNCurso.Connector
{
    public interface ITodoClient<T>
    {
        public Task<IEnumerable<T>> GetAllTodoItems();

        Task<Result> CreateTodoItem(TodoItemDto todoItem);

        Task<TodoItemDto> GetAsync(long value);
    }
}
