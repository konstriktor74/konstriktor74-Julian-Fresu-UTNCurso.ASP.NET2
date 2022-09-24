using System.ComponentModel.DataAnnotations;
using System.Net;
using Microsoft.EntityFrameworkCore;
using UTNCurso.Data;

namespace UTNCurso.Models
{
    public class TodoItem
    {
        private readonly TodoContext _context;

        public TodoItem()
        {
        }

        public TodoItem(TodoContext context)
        {
            _context = context;
        }

        public long Id { get; set; }

        [Required]
        [MaxLength(10)]
        public string Task { get; set; }

        public bool IsCompleted { get; set; }

        [ConcurrencyCheck]
        public DateTime LastModifiedDate { get; set; }

        public async Task<IEnumerable<TodoItem>> GetAllAsync()
        {
            return await _context.TodoItem.ToListAsync();
        }

        public async ValueTask<bool> IsModelAvailableAsync()
        {
            return await ValueTask.FromResult(_context.TodoItem != null);
        }

        public async Task<TodoItem> GetAsync(long id)
        {
            return await _context.TodoItem
                .FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<Result> CreateAsync(TodoItem todoItem)
        {
            Result result = new Result();

            CheckInputIsValid(todoItem, result);

            if (result.IsSuccessful)
            {
                _context.Add(todoItem);
                await _context.SaveChangesAsync();
            }

            return result;
        }

        public async Task<Result> UpdateAsync(TodoItem todoItem)
        {
            Result result = new Result();
            CheckInputIsValid(todoItem, result);

            try
            {
                if (result.IsSuccessful)
                {
                    _context.Update(todoItem);
                    await _context.SaveChangesAsync();
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TodoItemExists(todoItem.Id))
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
            var todoItem = await GetAsync(id);

            if (todoItem == null)
            {
                result.AddError(string.Empty, "The task doesn't exist");
                result.SetStatus((int)HttpStatusCode.NotFound);

                return result;
            }

            _context.TodoItem.Remove(todoItem);
            await _context.SaveChangesAsync();

            return result;
        }

        private void CheckInputIsValid(TodoItem todoItem, Result result)
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
