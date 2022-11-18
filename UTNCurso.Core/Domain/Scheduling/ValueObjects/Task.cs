namespace UTNCurso.Core.Domain.Agendas.ValueObjects
{
    public record Task
    {
        public string Description { get; private set; }

        public bool IsCompleted { get; private set; }

        private Task(string description, bool isCompleted)
        {
            Description = description;
            IsCompleted = isCompleted;
        }

        public static Task Create(string description, bool isCompleted)
        {
            return new Task(description, isCompleted);
        }
    }
}
