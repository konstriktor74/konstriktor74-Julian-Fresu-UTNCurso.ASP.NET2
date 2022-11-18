using UTNCurso.Core.Domain.Agendas.Entities;
using UTNCurso.Core.DTOs;
using UTNCurso.Core.Interfaces;

namespace UTNCurso.Core.Mappers
{
    public class TodoItemMapper : IMapper<TodoItem, TodoItemDto>
    {
        public TodoItemDto MapDalToDto(TodoItem entity)
        {
            return new TodoItemDto
            {
                Id = entity.Id,
                IsCompleted = entity.Task.IsCompleted,
                LastModifiedDate = entity.LastModifiedDate,
                Task = entity.Task.Description
            };
        }

        public IEnumerable<TodoItemDto> MapDalToDto(IEnumerable<TodoItem> entities)
        {
            foreach (var item in entities)
            {
                yield return MapDalToDto(item);
            }
        }

        public IEnumerable<TodoItemDto> MapDalToDto(IReadOnlyCollection<TodoItem> entities)
        {
            throw new NotImplementedException();
        }

        public TodoItem MapDtoToDal(TodoItemDto dto)
        {
            return TodoItem.Create(dto.Task, dto.IsCompleted, dto.LastModifiedDate, dto.Id);
        }
    }
}
