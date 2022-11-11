namespace UTNCurso.Core.Domain.Agendas.ValueObjects
{
    public record Task
    {
        public string Description { get; set; }

        public bool IsCompleted { get; set; }
    }
}
