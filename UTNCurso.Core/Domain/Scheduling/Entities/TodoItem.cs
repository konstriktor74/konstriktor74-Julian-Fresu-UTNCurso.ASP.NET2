namespace UTNCurso.Core.Domain.Agendas.Entities
{
    public class TodoItem : Entity<long>
    {
        private TodoItem()
        {
        }

        private TodoItem(ValueObjects.Task task, DateTime? lastModifiedDate, long id = default)
        {
            Id= id;
            Task = task;
            LastModifiedDate = lastModifiedDate;
        }

        public static TodoItem Create(string description, bool isCompleted)
        {
            return new TodoItem(
                ValueObjects.Task.Create(description, isCompleted),
                DateTime.UtcNow);
        }

        public static TodoItem Create(string description, bool isCompleted, DateTime? lastModifiedDate, long id)
        {
            return new TodoItem(
                ValueObjects.Task.Create(description, isCompleted),
                lastModifiedDate,
                id);
        }

        public void Update(ValueObjects.Task task)
        {
            Task = task;
            LastModifiedDate = DateTime.UtcNow;
        }

        public ValueObjects.Task Task { get; private set; }

        public DateTime? LastModifiedDate { get; private set; }

        public byte[] RowVersion { get; private set; }
    }
}