using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UTNCurso.Common.Entities;

namespace UTNCurso.DAL.EFCore.Configurations
{
    internal class TodoItemEntityConfiguration : IEntityTypeConfiguration<TodoItem>
    {
        public void Configure(EntityTypeBuilder<TodoItem> builder)
        {
            builder.Property(x => x.Task)
                .IsRequired()
                .HasMaxLength(10);

            builder.Property(x => x.RowVersion)
                .HasDefaultValue(0)
                .IsRowVersion();
        }
    }
}
