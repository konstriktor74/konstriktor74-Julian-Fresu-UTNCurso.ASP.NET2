using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using UTNCurso.Models;

namespace UTNCurso.Data
{
    public class TodoContext : IdentityDbContext<User>
    {
        public TodoContext (DbContextOptions<TodoContext> options)
            : base(options)
        {
        }

        public DbSet<UTNCurso.Models.TodoItem> TodoItem { get; set; } = default!;
    }
}
