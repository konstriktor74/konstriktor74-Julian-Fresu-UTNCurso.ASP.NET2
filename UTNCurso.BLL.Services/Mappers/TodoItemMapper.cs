using UTNCurso.BLL.DTOs;
using UTNCurso.Common.Entities;

namespace UTNCurso.BLL.Services.Mappers
{
    public class TodoItemMapper : IMapper<TodoItem, TodoItemDto>
    {
        public TodoItemDto MapDalToDto(TodoItem entity)
        {
            return new TodoItemDto
            {
                Id = entity.Id,
                IsCompleted = entity.IsCompleted,
                LastModifiedDate = entity.LastModifiedDate,
                Task = entity.Task
            };
        }

        public IEnumerable<TodoItemDto> MapDalToDto(IEnumerable<TodoItem> entities)
        {
            foreach (var item in entities)
            {
                yield return MapDalToDto(item);
            }
        }

        public TodoItem MapDtoToDal(TodoItemDto dto)
        {
            return new TodoItem
            {
                Id = dto.Id,
                IsCompleted = dto.IsCompleted,
                LastModifiedDate = dto.LastModifiedDate,
                Task = dto.Task
            };
        }
    }
}
