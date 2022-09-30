using System.Net;
using Microsoft.EntityFrameworkCore;
using UTNCurso.BLL.DTOs;
using UTNCurso.BLL.POCOs;
using UTNCurso.BLL.Services.Interfaces;
using UTNCurso.BLL.Services.Mappers;
using UTNCurso.Common.Entities;
using UTNCurso.DAL.EFCore;

namespace UTNCurso.BLL.Services
{
    public class TodoItemService : ITodoItemService
    {
        private readonly TodoContext _context;
        private readonly IMapper<TodoItem, TodoItemDto> _mapper;

        public TodoItemService(
            TodoContext context,
            IMapper<TodoItem, TodoItemDto> mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TodoItemDto>> GetAllAsync()
        {
            return _mapper.MapDalToDto(await _context.TodoItem.ToListAsync());
        }

        public async ValueTask<bool> IsModelAvailableAsync()
        {
            return await ValueTask.FromResult(_context.TodoItem != null);
        }

        public async Task<TodoItemDto> GetAsync(long id)
        {
            return _mapper.MapDalToDto(await _context.TodoItem
                .FirstOrDefaultAsync(m => m.Id == id));
        }

        public async Task<Result> CreateAsync(TodoItemDto todoItemdto)
        {
            Result result = new Result();

            CheckInputIsValid(todoItemdto, result);

            if (result.IsSuccessful)
            {
                _context.Add(_mapper.MapDtoToDal(todoItemdto));
                await _context.SaveChangesAsync();
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
                    _context.Update(_mapper.MapDtoToDal(todoItemDto));
                    await _context.SaveChangesAsync();
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TodoItemExists(todoItemDto.Id))
                {
                    result.SetStatus((int)HttpStatusCode.NotFound);

                    return result;
                }
                else
                {
                    throw;
                }
            }

            return result;
        }

        public async Task<Result> RemoveAsync(long id)
        {
            Result result = new Result();
            var todoItemDto = await GetAsync(id);

            if (todoItemDto == null)
            {
                result.AddError(string.Empty, "The task doesn't exist");
                result.SetStatus((int)HttpStatusCode.NotFound);

                return result;
            }

            _context.TodoItem.Remove(_mapper.MapDtoToDal(todoItemDto));
            await _context.SaveChangesAsync();

            return result;
        }

        private void CheckInputIsValid(TodoItemDto todoItem, Result result)
        {
            if (todoItem.Task.StartsWith("*"))
            {
                result.AddError("Task", "Cannot use asterisk");
            }
        }

        private bool TodoItemExists(long id)
        {
            return _context.TodoItem.Any(e => e.Id == id);
        }
    }
}