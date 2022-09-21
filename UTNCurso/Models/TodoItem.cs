using System.ComponentModel.DataAnnotations;

namespace UTNCurso.Models
{
    public class TodoItem
    {
        public long Id { get; set; }

        [Required]
        [MaxLength(10)]
        public string Task { get; set; }

        public bool IsCompleted { get; set; }

        [ConcurrencyCheck]
        public DateTime LastModifiedDate { get; set; }
    }
}
