using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using UTNCurso.Core.Domain.Agendas.Aggregates;
using UTNCurso.Core.Domain.Agendas.Entities;
using UTNCurso.Core.Domain.Users;
using UTNCurso.Infrastructure.Configurations;

namespace UTNCurso.Infrastructure
{
    public class TodoContext : IdentityDbContext<User>
    {
        public TodoContext(DbContextOptions<TodoContext> options)
            : base(options)
        {
            Database.Migrate();
        }

        public DbSet<Agenda> Agenda { get; set; }

        public DbSet<TodoItem> TodoItem { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfiguration(new AgendaEntityConfiguration());
            //builder.ApplyConfiguration(new TodoItemEntityConfiguration());

            builder.Entity<Agenda>().HasData(new List<Agenda> { Core.Domain.Agendas.Aggregates.Agenda.Create() });
        }
    }
}
