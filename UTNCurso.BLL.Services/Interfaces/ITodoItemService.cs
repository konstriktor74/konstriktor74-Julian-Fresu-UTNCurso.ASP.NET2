using UTNCurso.BLL.DTOs;
using UTNCurso.BLL.POCOs;

namespace UTNCurso.BLL.Services.Interfaces
{
    public interface ITodoItemService
    {
        Task<IEnumerable<TodoItemDto>> GetAllAsync();

        ValueTask<bool> IsModelAvailableAsync();

        Task<TodoItemDto> GetAsync(long id);

        Task<Result> CreateAsync(TodoItemDto todoItemdto);

        Task<Result> UpdateAsync(TodoItemDto todoItemDto);

        Task<Result> RemoveAsync(long id);
    }
}
