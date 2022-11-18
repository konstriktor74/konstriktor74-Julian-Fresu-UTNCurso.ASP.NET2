using AutoMapper;
using UTNCurso.Core.Domain.Agendas.Entities;
using UTNCurso.Core.DTOs;
using UTNCurso.Core.Interfaces;

namespace UTNCurso.Core.Mappers
{
    public class TodoItemAutoMapper : IMapper<TodoItem, TodoItemDto>
    {
        private readonly IMapper _mapper;

        public TodoItemAutoMapper(IMapper mapper)
        {
            _mapper = mapper;
        }

        public TodoItemDto MapDalToDto(TodoItem entity)
        {
            return _mapper.Map<TodoItemDto>(entity);
        }

        public IEnumerable<TodoItemDto> MapDalToDto(IEnumerable<TodoItem> entities)
        {
            return _mapper.Map<IEnumerable<TodoItemDto>>(entities);
        }

        public IEnumerable<TodoItemDto> MapDalToDto(IReadOnlyCollection<TodoItem> entities)
        {
            return _mapper.Map<IEnumerable<TodoItemDto>>(entities);
        }

        public TodoItem MapDtoToDal(TodoItemDto dto)
        {
            return _mapper.Map<TodoItem>(dto);
        }
    }
}
