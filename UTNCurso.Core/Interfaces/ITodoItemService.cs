using UTNCurso.Core.Domain;
using UTNCurso.Core.DTOs;

namespace UTNCurso.Core.Interfaces
{
    public interface ITodoItemService
    {
        Task<IEnumerable<TodoItemDto>> GetAllAsync();

        ValueTask<bool> IsModelAvailableAsync();

        Task<TodoItemDto> GetAsync(long id);

        Task<Result> CreateAsync(TodoItemDto todoItemdto);

        Task<Result> UpdateAsync(TodoItemDto todoItemDto);

        Task<Result> RemoveAsync(long id);

        Task<IEnumerable<TodoItemDto>> Search(string taskDescription, bool? isCompleted);
    }
}
