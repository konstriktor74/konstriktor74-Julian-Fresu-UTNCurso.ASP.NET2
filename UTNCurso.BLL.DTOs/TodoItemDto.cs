using System.ComponentModel.DataAnnotations;

namespace UTNCurso.BLL.DTOs
{
    public class TodoItemDto
    {
        public long Id { get; set; }

        [Required]
        [MaxLength(10)]
        public string Task { get; set; }

        public bool IsCompleted { get; set; }

        [ConcurrencyCheck]
        public DateTime? LastModifiedDate { get; set; }
    }
}
