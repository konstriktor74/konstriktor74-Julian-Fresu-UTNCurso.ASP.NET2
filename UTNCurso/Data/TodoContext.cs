using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using UTNCurso.Models;

namespace UTNCurso.Data
{
    public class TodoContext : DbContext
    {
        public TodoContext (DbContextOptions<TodoContext> options)
            : base(options)
        {
        }

        public DbSet<UTNCurso.Models.TodoItem> TodoItem { get; set; } = default!;
    }
}
