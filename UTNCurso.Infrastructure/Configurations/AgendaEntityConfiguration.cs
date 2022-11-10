using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UTNCurso.Core.Domain.Agendas.Aggregates;

namespace UTNCurso.Infrastructure.Configurations
{
    internal class AgendaEntityConfiguration : IEntityTypeConfiguration<Agenda>
    {
        public void Configure(EntityTypeBuilder<Agenda> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Ignore(x => x.Result);
            var todoItemConfig = builder.OwnsMany(x => x.TodoItems);
            builder.Navigation(x => x.TodoItems).UsePropertyAccessMode(PropertyAccessMode.Field);
            builder.Navigation(x => x.TodoItems).Metadata.SetField("_todoItems");

            todoItemConfig.OwnsOne(x => x.Task)
                .Property(x => x.Description)
                .IsRequired()
                .HasMaxLength(10);
            todoItemConfig.Property(x => x.LastModifiedDate)
                .HasDefaultValue(null);
            todoItemConfig.Property(x => x.RowVersion)
                .IsRowVersion();
        }
    }
}
