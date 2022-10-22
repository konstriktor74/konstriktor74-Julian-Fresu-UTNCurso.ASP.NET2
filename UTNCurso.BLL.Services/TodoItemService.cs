using System.Net;
using Microsoft.EntityFrameworkCore;
using UTNCurso.BLL.DTOs;
using UTNCurso.BLL.POCOs;
using UTNCurso.BLL.Services.Interfaces;
using UTNCurso.BLL.Services.Mappers;
using UTNCurso.Common.Entities;
using UTNCurso.DAL.EFCore;
using UTNCurso.DAL.EFCore.Repository;

namespace UTNCurso.BLL.Services
{
    public class TodoItemService : ITodoItemService
    {
        private readonly ITodoItemRepository _todoItemRepository;
        private readonly IMapper<TodoItem, TodoItemDto> _mapper;

        public TodoItemService(
            IMapper<TodoItem, TodoItemDto> mapper,
            ITodoItemRepository todoItemRepository)
        {
            _mapper = mapper;
            _todoItemRepository = todoItemRepository;
        }

        public async Task<IEnumerable<TodoItemDto>> GetAllAsync()
        {
            return _mapper.MapDalToDto(await _todoItemRepository.GetAll());
        }

        public async ValueTask<bool> IsModelAvailableAsync()
        {
            return await ValueTask.FromResult(_todoItemRepository != null);
        }

        public async Task<TodoItemDto> GetAsync(long id)
        {
            return _mapper.MapDalToDto(await _todoItemRepository.GetById(id));
        }

        public async Task<Result> CreateAsync(TodoItemDto todoItemdto)
        {
            Result result = new Result();

            CheckInputIsValid(todoItemdto, result);

            if (result.IsSuccessful)
            {
                await _todoItemRepository.Add(_mapper.MapDtoToDal(todoItemdto));
                await _todoItemRepository.SaveChangesAsync();
            }

            return result;
        }

        public async Task<Result> UpdateAsync(TodoItemDto todoItemDto)
        {
            Result result = new Result();
            CheckInputIsValid(todoItemDto, result);

            try
            {
                if (result.IsSuccessful)
                {
                    todoItemDto.LastModifiedDate = DateTime.UtcNow;
                    var entity = _mapper.MapDtoToDal(todoItemDto);
                    var rowEntity = await _todoItemRepository.GetById(todoItemDto.Id);
                    entity.RowVersion = rowEntity.RowVersion;
                    await _todoItemRepository.Update(entity);
                    await _todoItemRepository.SaveChangesAsync();
                }
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!await TodoItemExists(todoItemDto.Id))
                {
                    result.SetStatus((int)HttpStatusCode.NotFound);

                    return result;
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

                        await _todoItemRepository.SaveChangesAsync();
                    }
                    else
                    {
                        throw;
                    }
                }
            }

            return result;
        }

        public async Task<Result> RemoveAsync(long id)
        {
            Result result = new Result();
            var todoItem = await _todoItemRepository.GetById(id);

            if (todoItem == null)
            {
                result.AddError(string.Empty, "The task doesn't exist");
                result.SetStatus((int)HttpStatusCode.NotFound);

                return result;
            }

            await _todoItemRepository.Remove(todoItem.Id);
            await _todoItemRepository.SaveChangesAsync();

            return result;
        }

        private void CheckInputIsValid(TodoItemDto todoItem, Result result)
        {
            if (todoItem.Task.StartsWith("*"))
            {
                result.AddError("Task", "Cannot use asterisk");
            }
        }

        private async Task<bool> TodoItemExists(long id)
        {
            return await _todoItemRepository.GetById(id) != null;
        }
    }
}