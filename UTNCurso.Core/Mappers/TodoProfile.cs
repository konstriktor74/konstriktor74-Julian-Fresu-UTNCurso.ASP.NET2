using AutoMapper;
using UTNCurso.Core.Domain.Agendas.Entities;
using UTNCurso.Core.DTOs;

namespace UTNCurso.Core.Mappers
{
    public class TodoProfile : Profile
    {
        public TodoProfile()
        {
            CreateMap<TodoItemDto, TodoItem>()
                .ForMember(d => d.Task, opt => opt.Ignore())
                .ConstructUsing((d, s) => TodoItem.Create(d.Task, d.IsCompleted, d.LastModifiedDate, d.Id));

            CreateMap<TodoItem, TodoItemDto>()
                .ForMember(x => x.Task, opt => opt.MapFrom(s => s.Task.Description))
                .ForMember(x => x.IsCompleted, opt => opt.MapFrom(s => s.Task.IsCompleted));
        }
    }
}
