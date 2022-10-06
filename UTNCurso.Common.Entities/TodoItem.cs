using System.ComponentModel.DataAnnotations;

namespace UTNCurso.Common.Entities
{
    public class TodoItem
    {
        public long Id { get; set; }

        public string Task { get; set; }

        public bool IsCompleted { get; set; }

        public DateTime? LastModifiedDate { get; set; }

        public int RowVersion { get; set; }
    }
}