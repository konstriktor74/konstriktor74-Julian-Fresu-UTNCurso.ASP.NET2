using System.Net;
using UTNCurso.Core.Domain.Agendas.Entities;
using UTNCurso.Core.Interfaces;

namespace UTNCurso.Core.Domain.Agendas.Aggregates
{
    public class Agenda : Entity<Guid>, IAggregateRoot
    {
        private readonly List<TodoItem> _todoItems;

        public IReadOnlyCollection<TodoItem> TodoItems => _todoItems;
        public Result Result { get; private set; }

        public static Agenda Create()
        {
            return new Agenda { Id = Guid.NewGuid() };
        }
        private Agenda()
        {
            _todoItems = new List<TodoItem>();
            Result = new Result();
        }

        public void AddTodoItem(TodoItem item)
        {
            CheckTodoItemInputGuard(item);
            _todoItems.Add(item);
        }

        public TodoItem GetTodoItemById(long id)
        {
            var todoItem = _todoItems.FirstOrDefault(x => x.Id == id);
            TodoItemExistGuard(todoItem);

            return todoItem;
        }

        public IEnumerable<TodoItem> GetAllTodoItems()
        {
            return _todoItems;
        }

        public void UpdateTodoItem(TodoItem item)
        {
            CheckTodoItemInputGuard(item);
            var todoItem = _todoItems.FirstOrDefault(x => x.Id == item.Id);
            TodoItemExistGuard(todoItem);
            todoItem.Update(item.Task);
        }

        public void DeleteTodoItemById(long id)
        {
            var todoItem = _todoItems.FirstOrDefault(x => x.Id == id);
            TodoItemExistGuard(todoItem);
            _todoItems.Remove(todoItem);
        }

        private void TodoItemExistGuard(TodoItem todoItem)
        {
            Result = new Result();

            if (todoItem is null)
            {
                Result.AddError(string.Empty, "The task doesn't exist");
                Result.SetStatus((int)HttpStatusCode.NotFound);
            }
        }

        private void CheckTodoItemInputGuard(TodoItem todoItem)
        {
            Result = new Result();

            if (todoItem.Task.Description.StartsWith("*") || todoItem.Task.Description.Contains("#"))
            {
                Result.AddError("Task", "Cannot use asterisk or hashtag");
            }
        }
    }
}