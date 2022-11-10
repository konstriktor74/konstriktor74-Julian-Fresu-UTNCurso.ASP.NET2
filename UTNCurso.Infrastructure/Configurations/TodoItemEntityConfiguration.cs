using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UTNCurso.Core.Domain.Agendas.Entities;

namespace UTNCurso.Infrastructure.Configurations
{
    internal class TodoItemEntityConfiguration : IEntityTypeConfiguration<TodoItem>
    {
        public void Configure(EntityTypeBuilder<TodoItem> builder)
        {
            builder.HasKey(t => t.Id);
            builder.OwnsOne(x => x.Task)
                .Property(x => x.Description)
                .IsRequired()
                .HasMaxLength(10);
            builder.Property(x => x.LastModifiedDate)
                .HasDefaultValue(null);
            builder.Property(x => x.RowVersion)
                .IsRowVersion();
        }
    }
}
