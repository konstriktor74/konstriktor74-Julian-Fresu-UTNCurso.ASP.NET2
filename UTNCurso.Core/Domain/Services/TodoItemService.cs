using System.Data.Entity.Infrastructure;
using Microsoft.Extensions.Logging;
using UTNCurso.BLL.DTOs;
using UTNCurso.Core.Domain.Agendas.Entities;
using UTNCurso.Core.Interfaces;

namespace UTNCurso.Core.Domain.Services
{
    public class TodoItemService : ITodoItemService
    {
        private readonly IMapper<TodoItem, TodoItemDto> _mapper;
        private readonly IAgendaRepository _agendaRepository;
        private readonly ILogger<TodoItemService> _logger;

        public TodoItemService(
            IMapper<TodoItem, TodoItemDto> mapper,
            IAgendaRepository agendaRepository,
            ILogger<TodoItemService> logger)
        {
            _agendaRepository = agendaRepository;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TodoItemDto>> GetAllAsync()
        {
            var agendas = await _agendaRepository.GetAll();
            return _mapper.MapDalToDto(agendas.SelectMany(x => x.GetAllTodoItems()));
        }

        public async ValueTask<bool> IsModelAvailableAsync()
        {
            return await ValueTask.FromResult(_agendaRepository != null);
        }

        public async Task<TodoItemDto> GetAsync(long id)
        {
            var agendas = await _agendaRepository.GetAll();
            var agenda = agendas.FirstOrDefault();
            return _mapper.MapDalToDto(agenda.GetTodoItemById(id));
        }

        public async Task<Result> CreateAsync(TodoItemDto todoItemdto)
        {
            var agendas = await _agendaRepository.GetAll();
            var agenda = agendas.FirstOrDefault();
            agenda.AddTodoItem(_mapper.MapDtoToDal(todoItemdto));

            if (agenda.Result.IsSuccessful)
            {
                await _agendaRepository.SaveChangesAsync();
            }

            return agenda.Result;
        }

        public async Task<Result> UpdateAsync(TodoItemDto todoItemDto)
        {
            _logger.LogInformation("Updating todo item");
            var agendas = await _agendaRepository.GetAll();
            var agenda = agendas.FirstOrDefault();
            using (_logger.BeginScope("Trying to update"))
            {
                try
                {
                    todoItemDto.LastModifiedDate = DateTime.UtcNow;
                    var entity = _mapper.MapDtoToDal(todoItemDto);
                    agenda.UpdateTodoItem(entity);

                    if (agenda.Result.IsSuccessful)
                    {
                        await _agendaRepository.SaveChangesAsync();
                    }
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    if (agenda.GetTodoItemById(todoItemDto.Id) is null)
                    {
                        return agenda.Result;
                    }
                    else
                    {
                        if (ex.Entries.Any())
                        {
                            foreach (var entry in ex.Entries)
                            {
                                var dbValues = entry.GetDatabaseValues();
                                entry.OriginalValues.SetValues(dbValues);
                            }

                            await _agendaRepository.SaveChangesAsync();
                        }
                        else
                        {
                            throw;
                        }
                    }
                }
            }

            return agenda.Result;
        }

        public async Task<Result> RemoveAsync(long id)
        {
            var agendas = await _agendaRepository.GetAll();
            var agenda = agendas.FirstOrDefault();
            var todoItem = agenda.GetTodoItemById(id);

            if (todoItem == null)
            {
                return agenda.Result;
            }

            agenda.DeleteTodoItemById(todoItem.Id);

            if (agenda.Result.IsSuccessful)
            {
                await _agendaRepository.SaveChangesAsync();
            }

            return agenda.Result;
        }
    }
}